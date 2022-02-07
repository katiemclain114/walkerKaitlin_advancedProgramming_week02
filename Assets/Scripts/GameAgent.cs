using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//making this an abstract class to act as a template for different types of game agents
//ie player vs ai player
public abstract class GameAgent
{
    #region Variables

    //speed of gameAgent
    private const float MovementSpeed = 5.0f;

    //prefab of game agent sent to gameagent from game manager
    protected GameObject _gameObject;
    // TODO might not use this 
    //ref to game object renderer for setting material of different team member
    private Renderer _renderer;
    //ref to game object rigidbody
    protected Rigidbody _rigidbody;
    //start location of game agent
    private Vector3 _startingPosition;
    // => points to variable "constantly" unlike setting it equal to  variable once 
    public Vector3 position => _gameObject.transform.position;

    //TODO might not use multiple teams 
    [HideInInspector]
    public int teamID;

    #endregion

    #region Lifestyle Management

    //when game agent is created with game object set gameAgent variables to gameObject variables
    protected GameAgent(GameObject gameObject)
    {
        _gameObject = gameObject;
        _gameObject.transform.parent = Services.GameManager.parentGameAgents.transform;
        _renderer = _gameObject.GetComponent<Renderer>();
        _rigidbody = _gameObject.GetComponent<Rigidbody>();
    }

    //update function to be overriden 
    public abstract void Update();

    //destroy gameObject
    public void Destroy()
    {
        Object.Destroy(_gameObject);
    }

    #endregion

    #region Core Funtions

    //TODO might not use teams
    //sets game agent team
    public GameAgent SetTeam(int teamID, bool isPlayer = false)
    {
        this.teamID = teamID;
        _renderer.material = isPlayer ? Services.GameManager.playerMaterial : Services.GameManager.teamMaterials[teamID];

        return this;
    }
    
    //sets game agent position and starting position
    public GameAgent SetPosition(float x, float y, float z, bool isStartingPosition = false)
    {
        _gameObject.transform.position = new Vector3(x, y, z);

        if (isStartingPosition)
        {
            _startingPosition = new Vector3(x, y, z);
        }
        
        return this;
    }
    
    //reset game agent to start position
    public void SetToStartingPosition()
    {
        _gameObject.transform.position = _startingPosition;
    }
    
    //moves position of game agent in direction given to function
    protected void MoveInDirection(Vector3 direction)
    {
        var newPosition = _gameObject.transform.position;

        newPosition += Time.deltaTime * MovementSpeed * direction;

        _rigidbody.MovePosition(newPosition);
    }

    #endregion
}

public class AIPlayer : GameAgent
{
    //transform of coin that player is tracking
    private Transform currentTarget;

    public AIPlayer(GameObject gameObject) : base(gameObject)
    {
        //no changes at moment
    }

    public override void Update()
    {
        if (currentTarget != null)
        {
            //gets direction to target from game object 
            MoveInDirection((currentTarget.position - _gameObject.transform.position).normalized);
        }
        else
        {
            currentTarget = Services.AIManager.GetClosestGold(this)?.transform;
        }
    }
}

public class Player : GameAgent
{
    public Player(GameObject gameObject) : base(gameObject)
    {
        // no change
    }

    public override void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        MoveInDirection(new Vector3(horizontal, 0, vertical));
    }
}
