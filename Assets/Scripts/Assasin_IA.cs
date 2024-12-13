using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Assasin_IA : MonoBehaviour
{
    public Transform target; // El destino hacia donde moverse.
    public NavMeshAgent agent;
    public int Behavior_Decition;
    public int Option;
    public int Damage;
    public float Cooldown_Decition;
    public float GameTime;
    public float Agrofeeling;
    public float Multiplier;
    public float SpeedMultiplier;
    public bool visible;
    public bool huntingMode;

    public Transform Victim;
    public GameObject VictimGameObject;
    public bool VictimTarged;



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
        VictimTarged = false;
        Multiplier = 1f;
        SpeedMultiplier = 1f;
        Damage = 10;

    }

    // Update is called once per frame
    void Update()
    {

        IaSystem();

    }

    void HuntingBeavior()
    {
        if (VictimTarged)
        {
            if (agent.isStopped)
            {
                agent.isStopped = false;
                
                if (Behavior_Decition != 99)
                {
                    SpeedMultiplier = 5.5f;
                    agent.speed = SpeedMultiplier;
                }
                else
                {
                    SpeedMultiplier = 12f;
                    agent.speed = SpeedMultiplier;
                }
            }
            if (Behavior_Decition != 99)
            {
                agent.destination = Victim.position;

                if (agent.remainingDistance <= 0)
                {
                    if (VictimGameObject.GetComponent<Npcs_IA>().Health - Damage > 0)
                    {
                        VictimGameObject.GetComponent<Npcs_IA>().Health -= Damage * Time.deltaTime;
                    }
                    else
                    {
                        CleanActions();
                        VictimGameObject.GetComponent<Npcs_IA>().Health = 0;
                        VictimGameObject = null;
                        Victim = null;
                    }

                }
            }
            else
            {
                agent.destination = Prota.transform.position;
            }
            

            //Validador de llegada a objetivo

            

        }
        else
        {
            SpeedMultiplier = 4f;
            agent.speed = SpeedMultiplier; 
        }
        
    }

    void CleanActions()
    {
        if (VictimTarged)
        {
            VictimTarged = false;
        }
        if (huntingMode)
        {
            huntingMode = false;
        }
        Cooldown_Decition = 0;
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
        Cooldown_Decition = Random.Range(10f, 18f);

        //chill posibilities -*-
        if (BA_Number == 1)
        {
            if (huntingMode)
            {
                huntingMode = false;
            }
            repositionTarget();
            while(CurrentPoint.GetComponent<Area_Script>().Location_Id == Prota.GetComponent<Location>().Location_Id)
            {
                repositionTarget();
            }

        }
        if (BA_Number == 2)
        {
            if (huntingMode)
            {
                huntingMode = false;
            }
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
            huntingMode = true;
        }
        //Soft posibilities -**-
        if (BA_Number == 99)
        {
            huntingMode = true;
            VictimTarged = true;
            agent.isStopped = true;

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
                Agrofeeling = Agrofeeling + (Random.Range(1f, 5f) * Multiplier) ;
            }
            if (GameTime >= 60f && GameTime < 300f) // lets start to move a little more 
            {
                Agrofeeling = Agrofeeling + (Random.Range(5f, 15f) * Multiplier);
            }
            if (GameTime >= 300f && GameTime < 450f) // ......
            {
                Agrofeeling = Agrofeeling + (Random.Range(15f, 30f) * Multiplier);
            }
            //else // Enough of you....
            //{
            //    Agrofeeling = Agrofeeling + Random.Range(40f, 60f);
            //}


            //Limpieza de booleanos de accion

            // ==>>>>>>>>>>>> CleanActions();
            

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

                Behavior_Decition = 4;
            }
            if (Agrofeeling >= 50f && Agrofeeling < 100f)
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

                Behavior_Decition = 4;
            }
            if (Agrofeeling >= 100f)
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

                Behavior_Decition = 99;
            }
            //else //Time to die -X-
            //{
            //    //Bloodlust Mode
            //}

            Behavior_Action(Behavior_Decition);

        }

        else
        {
            if (Behavior_Decition != 99)
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

        if (huntingMode)
        {
            HuntingBeavior();
        }
    }
}
