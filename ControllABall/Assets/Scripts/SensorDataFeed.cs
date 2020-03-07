using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using System.Globalization;

public class SensorDataFeed : MonoBehaviour
{
    // Serial port initializaton. HC-05 Bluetooth module creates 2 serial COM ports when paired. (Check for active COM ports in device managed on WIN)
    SerialPort stream = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One);

    GameObject ground;
    GameObject wall;
    GameObject player;

    // Container variables for remote data
    float forceX = 0, forceY = 0;

    Thread childThread;


    void Start()
    {
        // We need 'ground' and 'wall' references to manipulate/change transform (angle) data
        ground = GameObject.Find("Ground");
        wall = GameObject.Find("Walls");

        // We need player reference to add force to player/ball
        player = GameObject.Find("Player");

        // open the serial communication
        stream.Open();

        // We need to start a thread for incomming data.
        // It would freeze the game if we wait for incomming data in main game thread.
        ThreadStart childref = new ThreadStart(FeedThread);
        childThread = new Thread(childref);
        childThread.Start();


    }

    public void FeedThread()
    {
        while (true)
        {
            // Incomming data is in format: "foceX,forceY". We need to split input data.
            string value = stream.ReadLine(); // read the data out of serial port
            string[] forces = value.Split(',');

            // We can't access game objects from thread, so we need to save both values.
            // Those values will be used when engine renders the very next frame.
            forceX = (float.Parse(forces[0], CultureInfo.InvariantCulture.NumberFormat) / 50000) * 50; // Some data manipulation is needed to get the right values for the eulerAngles method
            forceY = (float.Parse(forces[1], CultureInfo.InvariantCulture.NumberFormat) / 50000) * 50;
        }

    }

    // Update is called once per frame
    void Update()
    {
        // If we have got new values from remote controller, we apply those values to 'ground', 'wall' and 'player' objects
        if (forceX != 0 || forceY != 0)
        {
            ground.transform.eulerAngles = new Vector3(forceX, 0f, forceY);
            wall.transform.eulerAngles = new Vector3(forceX, 0f, forceY);

            player.GetComponent<Rigidbody>().AddForce(forceX * 3, 0f, forceY * 3);

            forceX = 0f;
            forceY = 0f;
        }
    }


    void OnApplicationQuit()
    {
        if (childThread != null)
        {
            childThread.Abort();
        }
        if (stream != null)
        {
            stream.Close();
        }

    }
}
