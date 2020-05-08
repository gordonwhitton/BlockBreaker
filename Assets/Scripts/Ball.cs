using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle1; //the class we already created
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 10f;

    [SerializeField] AudioClip[] ballSounds; //we will assing this in Unity

    [SerializeField] float randomFactor = 0.2f;


    //state - distance between paddle and ball
    Vector2 paddleToBallVector;

    private bool hasStarted = false;

    // cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;


    // Start is called before the first frame update
    void Start()
    {
        //refers to the transform position we have on the ball minus the paddle possition
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(! hasStarted)
        {
            LockBallToPaddle(); // only want to do this the first time
            LaunchOnMouseClick();
        }
    }

    //we only want this to happen the once
    private void LaunchOnMouseClick()
    {
        if(Input.GetMouseButtonDown(0))//0 is the left button
        {
            hasStarted = true;

            //push the ball, 2f so it goes a little bit to the right
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePossition = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);

        //give the ball it's location
        transform.position = paddlePossition + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));

        if(hasStarted)
        {
            //GetComponent<AudioSource>().Play();
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip); //want to play audio all the way through, don't want it overlapping, even if other things start to play
            myRigidBody2D.velocity += velocityTweak; //adds the tweak to the velocity
        }
    }
}
