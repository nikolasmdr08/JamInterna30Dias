using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Assasin_IA : MonoBehaviour
{
    public Transform target; // El destino hacia donde moverse.
    private NavMeshAgent agent;
    public int Behavior_Decition;
    public int Option;
    public float Cooldown_Decition;
    public float GameTime;
    public float Agrofeeling;
    public bool visible;
    public bool huntingMode;



    public GameObject[] ObjetiveZones;
    public GameObject CurrentPoint;
    int Index;
    public GameObject Prota;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Behavior_Decition = 0;
        Cooldown_Decition = 0;
        GameTime = 0;
        Agrofeeling = 0;
        ObjetiveZones = GameObject.FindGameObjectsWithTag("Zones");
        Prota = GameObject.FindGameObjectWithTag("MainCharacter");
        visible = true;
        huntingMode = false;

    }

    // Update is called once per frame
    void Update()
    {

        IaSystem();

        if (huntingMode)
        {
            HuntingBeavior();
        }

    }

    void HuntingBeavior()
    {
        //throw new System.NotImplementedException();
    }

    void repositionTarget ()
    {       
        Index = Random.Range(0, ObjetiveZones.Length);
        CurrentPoint = ObjetiveZones[Index];
        var randomPos = new Vector3( Random.Range(CurrentPoint.GetComponent<BoxCollider>().bounds.min.x, CurrentPoint.GetComponent<BoxCollider>().bounds.max.x), 0f, Random.Range(CurrentPoint.GetComponent<BoxCollider>().bounds.min.z, CurrentPoint.GetComponent<BoxCollider>().bounds.max.z));
        target.position = randomPos;
    }

    void Behavior_Action( int BA_Number)
    {
        Cooldown_Decition = Random.Range(15f, 40f);

        //chill posibilities -*-
        if (BA_Number == 1)
        {
            repositionTarget();
            while(CurrentPoint.GetComponent<Area_Script>().Location_Id == Prota.GetComponent<Location>().Location_Id)
            {
                repositionTarget();
            }

        }
        if (BA_Number == 2)
        {
            repositionTarget();
            while (CurrentPoint.GetComponent<Area_Script>().Location_Id == Prota.GetComponent<Location>().Location_Id)
            {
                repositionTarget();
            }
            this.transform.position = target.position;
            print("quiero tepearme");
            //target.position = Vector3.zero;
            visible = false;
        }
        if (BA_Number == 3)
        {
            repositionTarget();
            while (CurrentPoint.GetComponent<Area_Script>().Location_Id == Prota.GetComponent<Location>().Location_Id)
            {
                repositionTarget();
            }
            huntingMode = true;

        }
        if (BA_Number == 4)
        {
            repositionTarget();
        }
        //Soft posibilities -**-
        if (BA_Number == 5)
        {
            repositionTarget();
        }
        //Medium posibilities -***-
        //Hard posibilities -****-
        //Very Hard posibilities -*****-
        //Time to die -X-
    }

    void IaSystem()
    {
        if (Behavior_Decition == 0 && Cooldown_Decition == 0)
        {
            //Killer Thinking: I want to play with him.... let's have some fun..
            //Acumulative posibilities

            if (GameTime < 60f) // I will start easy 
            {
                Agrofeeling = Agrofeeling + Random.Range(1f, 5f);
            }
            if (GameTime >= 60f && GameTime < 300f) // lets start to move a little more 
            {
                Agrofeeling = Agrofeeling + Random.Range(5f, 15f);
            }
            if (GameTime >= 300f && GameTime < 450f) // ......
            {
                Agrofeeling = Agrofeeling + Random.Range(15f, 30f);
            }
            //else // Enough of you....
            //{
            //    Agrofeeling = Agrofeeling + Random.Range(40f, 60f);
            //}




            //My next move will be...

            if (Agrofeeling < 15f)
            {
                //chill posibilities -*-

                Option = Random.Range(1, 3); //cant opciones

                // 1)I will give him space
                // if Prota esta en un sector , me ire al mas alejado
                if (Option == 1)
                {
                    Behavior_Decition = 1;
                }

                // 2)I will give him space
                // if Prota esta en un sector , me ire al mas alejado
                if (Option == 2)
                {
                    Behavior_Decition = 2;
                }


            }
            if (Agrofeeling >= 15f && Agrofeeling < 30f)
            {
                //Soft posibilities -**-

                // 1)I will give him space
                // if Prota esta en un sector , me ire al mas alejado
                Behavior_Decition = 3;

                // 2)I will give him space
                // if Prota esta en un sector , me ire al mas alejado

                // 3)I will give him space
                // if Prota esta en un sector , me ire al mas alejado

                // 4)I will give him space
                // if Prota esta en un sector , me ire al mas alejado


            }
            if (Agrofeeling >= 30f && Agrofeeling < 50f)
            {
                //Medium posibilities -***-

                // 1)I will give him space
                // if Prota esta en un sector , me ire al mas alejado

                // 2)I will give him space
                // if Prota esta en un sector , me ire al mas alejado

                // 3)I will give him space
                // if Prota esta en un sector , me ire al mas alejado

                // 4)I will give him space
                // if Prota esta en un sector , me ire al mas alejado

                Behavior_Decition = 5;
            }
            if (Agrofeeling >= 50f && Agrofeeling < 70f)
            {
                //Hard posibilities -****-

                // 1)I will give him space
                // if Prota esta en un sector , me ire al mas alejado

                // 2)I will give him space
                // if Prota esta en un sector , me ire al mas alejado

                // 3)I will give him space
                // if Prota esta en un sector , me ire al mas alejado

                // 4)I will give him space
                // if Prota esta en un sector , me ire al mas alejado

                Behavior_Decition = 5;
            }
            if (Agrofeeling >= 70f && Agrofeeling < 85f)
            {
                //Very Hard posibilities -*****-

                // 1)I will give him space
                // if Prota esta en un sector , me ire al mas alejado

                // 2)I will give him space
                // if Prota esta en un sector , me ire al mas alejado

                // 3)I will give him space
                // if Prota esta en un sector , me ire al mas alejado

                // 4)I will give him space
                // if Prota esta en un sector , me ire al mas alejado

                Behavior_Decition = 5;
            }
            //else //Time to die -X-
            //{
            //    //Bloodlust Mode
            //}

            Behavior_Action(Behavior_Decition);

        }

        else
        {
            if (Cooldown_Decition >= 0)
            {
                Cooldown_Decition = Cooldown_Decition - Time.deltaTime;
            }
            else
            {
                Behavior_Decition = 0;
                Cooldown_Decition = 0;
                if (visible == false)
                {
                    visible = true;
                    agent.isStopped = false;
                }

            }
        }

        if (target.position != Vector3.zero)
        {
            agent.SetDestination(target.position);
        }
        if (!visible)
        {
            agent.isStopped = true;
        }

        GameTime = GameTime + Time.deltaTime;
    }
}
