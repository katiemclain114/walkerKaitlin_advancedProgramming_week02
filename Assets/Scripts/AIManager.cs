using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIManager
{
    //list of all game agents in game
    private List<GameAgent> _gameAgents;
    private List<Gold> _golds;
    private float timer_GoldShower = 10;
    private float timerTotal_GoldShower = 10;

    public void Initialize()
    {
        _gameAgents = new List<GameAgent>();
        _golds = new List<Gold>();
        _CreateAIPlayers();
    }
    
    public void Update()
    {
        //runs each agent's update
        foreach (var agent in _gameAgents)
        {
            agent.Update();
        }
        //runs player update
        foreach (var player in Services.Players)
        {
            player.Update();
        }

        //checks timer for new gold spawn
        //updates timer
        if (timer_GoldShower > timerTotal_GoldShower)
        {
            timer_GoldShower = 0;
            _CreateGoldShower();
        }
        else
        {
            timer_GoldShower += Time.deltaTime;
        }
    }
    
    //destroy all game agents
    public void DestroyAllObjects()
    {
        foreach (var agent in _gameAgents)
        {
            agent.Destroy();
        }
        _gameAgents.Clear();

        foreach (var gold in _golds)
        {
            Object.Destroy(gold.gameObject);
        }
        
        _golds.Clear();
    }
    
    
    //returns closest gold
    //this is very similar to what i originally did so not confusing 
    //basically check distance of each gold to gameAgent - 
    //if gold closer than last gold make new closest gold
    public Gold GetClosestGold(GameAgent gameAgent)
    {
        if (_golds.Count == 0) return null;

        var closest = _golds[0];
        var distance = float.MaxValue;

        foreach (var gold in _golds)
        {
            var distanceToPlayer = Vector3.Distance(gold.transform.position, gameAgent.position);
            if (distanceToPlayer < distance)
            {
                closest = gold;
                distance = distanceToPlayer;
            }
        }

        return closest;
    }
    
    //destroy gold gameObject and remove from list
    public void DestroyGold(Gold gold)
    {
        if (!_golds.Contains(gold)) return;
        _golds.Remove(gold);
        Object.Destroy(gold.gameObject);
    }

    private void _CreateAIPlayers()
    {
        foreach (var player in Services.Players)
        {
            player.SetTeam(player.teamID, true);
            _gameAgents.Add(player);
        }
        //makes two teams hence teamID < 2
        for(int teamID = 0; teamID < 2; teamID++)
        {
            // Make AI players
            
            for (var i = 0; i < GameManager.GameSetting_PlayersPerTeam; i++)
            {
                var createdGameObject = Object.Instantiate(Services.GameManager.prefab_GameAgent);
                createdGameObject.name = "Player_" + teamID + "_" + i;
                createdGameObject.tag = "GameAgent_" + teamID;
                _gameAgents.Add(new AIPlayer(createdGameObject).SetTeam(teamID).SetPosition(Random.Range(-7.0f, 7.0f), 1, Random.Range(-3.0f, 3.0f), true));
            }
        }
    }
    
    
    //pretty basic spawn gold function 
    private void _CreateGoldShower()
    {
        // Make gold
        for (var i = 0; i < GameManager.GameSetting_GoldShowerAmount; i++)
        {
            var createdGameObject = Object.Instantiate(Services.GameManager.prefab_Gold);
            createdGameObject.name = "Gold_" + i + "_" + Time.deltaTime;
            createdGameObject.transform.position = new Vector3(Random.Range(-7.0f, 7.0f), 1, Random.Range(-3.5f, 3.5f));
            _golds.Add(createdGameObject);
        }
    }
}
