using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {
    public WheelCollider FrontLeftWheel;
    public WheelCollider FrontRightWheel;
    public WheelCollider BackLeftWheel;
    public WheelCollider BackRightWheel;

    public Transform FrontLeftWheelModel;
    public Transform FrontRightWheelModel;

    public float EngineTorque = 5f;
    public float SteeringAngle = 5f;

	// Use this for initialization
	void Start () {
        FixCar();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            BackLeftWheel.motorTorque = EngineTorque;
            BackRightWheel.motorTorque = EngineTorque;
//            GetComponent<ConstantForce>().relativeForce = Vector3.forward * 120 * 20;
        }
        else
        {
            BackLeftWheel.motorTorque = 0f;
            BackRightWheel.motorTorque = 0f;
            //            GetComponent<ConstantForce>().relativeForce = Vector3.zero;
        }

        if(Input.GetKey(KeyCode.S))
        {
            BackLeftWheel.brakeTorque = 90f;
            BackRightWheel.brakeTorque = 90f;
        }
        else
        {
            BackLeftWheel.brakeTorque = 0f;
            BackRightWheel.brakeTorque = 0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            FrontLeftWheel.steerAngle = -SteeringAngle;
            FrontRightWheel.steerAngle = -SteeringAngle;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            FrontLeftWheel.steerAngle = SteeringAngle;
            FrontRightWheel.steerAngle = SteeringAngle;
        }
        else
        {
            FrontLeftWheel.steerAngle = 0;
            FrontRightWheel.steerAngle = 0;
        }

        UpdateTireMeshes();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            FixCar();
        }
    }

    void UpdateTireMeshes()
    {
        FrontLeftWheelModel.localRotation = Quaternion.Euler(
            FrontLeftWheelModel.localRotation.eulerAngles.x,
            FrontLeftWheel.steerAngle,
            FrontLeftWheelModel.localRotation.eulerAngles.z);
        FrontRightWheelModel.localRotation = Quaternion.Euler(
            FrontRightWheelModel.localRotation.eulerAngles.x,
            FrontLeftWheel.steerAngle,
            FrontRightWheelModel.localRotation.eulerAngles.z);
    }

    void FixCar()
    {
        
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = new Vector3(transform.position.x, GameManager.instance.activeTerrain.SampleHeight(transform.position)+1, transform.position.z);
        transform.localRotation = Quaternion.Euler(0f, transform.localRotation.eulerAngles.y, 0f);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = false;

    }
}
