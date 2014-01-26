﻿using UnityEngine;
using System.Collections;

public class Wood_board_float : MonoBehaviour {

    private float y = 1;
    private double wood_cap;
    public Water_flow flow;
    private float water_rate1 = 0.5F;
    private float water1 = 0.0F;
    private float water_rate = 0.5F;
    private float water = 0.0F;
    public World_bot_pos world_bot_pos;
    Vector3 planky_move = new Vector3(0,0,0);
    private int check;
    public int check2;
    private bool check3 = true;
    private bool down = false;
	// Use this for initialization
	void Start () {
        //planky_move = transform.position.y;
        wood_cap = transform.position.y - 2.5;
        flow = GameObject.Find("water").GetComponent<Water_flow>();
        world_bot_pos = GameObject.Find("WorldBorder Bottom").GetComponent<World_bot_pos>();
        check = 0;
	}



        void OnTriggerEnter2D(Collider2D other)
    {   
        //planky_move = new Vector3(0, 0, 0);

    
            if (other.gameObject.name == "water")
            {
                //Debug.Log(check);
                if (check == 0)
                {
                    check = 1;
                }
                if (check == 3)
                {
                    check = 1;
                }
            }
        

        if (other.gameObject.name == "Rock_platform1")
        {

            
            planky_move = new Vector3(0,0,0);
            if (!down)
            {
                check = 2;
            }
            if((down))
            {
                check = 3;
            }

        }

        if (other.gameObject.name == "Rock_platform2")
        {


            planky_move = new Vector3(0, 0, 0);
            if (!down)
            {
                check = 2;
            }
            if ((down))
            {
                check = 3;
            }

        }
        if (other.gameObject.name == "Rock_platform3")
        {


            planky_move = new Vector3(0, 0, 0);
            if (!down)
            {
                check = 2;
            }
            if ((down))
            {
                check = 3;
            }

        }
        if (other.gameObject.name == "WorldBorder Bottom")
        {

            Debug.Log("enter");
            planky_move = new Vector3(0, 0, 0);
            check3 = false;
            check = 0;
        }
        /*
        if(!(other.gameObject.name == "water"))
        {
            Debug.Log("water");
            planky_move -= new Vector3(0, 1, 0);

        }
         */
        //transform.position += planky_move;
    }

    /*
        void OnTriggerExit2D(Collider2D other)
        {
            check += 1;
            Debug.Log(check);
            if (check==2)
            {
                Debug.Log("check");
                if (other.gameObject.name == "water")
                {
                    Debug.Log("in here");
                    planky_move += new Vector3(0, -1, 0);
                    //transform.position -= new Vector3(0, 1, 0);
                }
                transform.position += planky_move;
            }
        }
    
    */
	// Update is called once per frame
	void Update () {
        planky_move = new Vector3(0, 0, 0);
        Vector3 water_level = flow.transform.position;
        
            if (check == 1)
            {
                if ((flow.transform.position.y - transform.position.y) <= 0.65F)
                {
                if (Time.time > water)
                {
                    water = Time.time + water_rate;
                    
                        //Debug.Log("level_water_cap");
                        transform.position += new Vector3(0, y, 0);
                        check3 = true;
                        down = false;
                    
                }
                //planky_move += new Vector3(0, 2.65f, 0);
                //transform.position.y = flow.transform.position.y + planky_move.y; // new Vector3(0, 2.65, 0);
            }
        }
            if (check == 2)
            {
                if ((flow.transform.position.y < transform.position.y) && ((water_level.y + -2.22) < transform.position.y) && (check2 == 1) && (check3))
                {
                    if (Time.time > water)
                    {
                        water = Time.time + water_rate;

                        //Debug.Log("level_water_cap");
                        transform.position -= new Vector3(0, y, 0);
                        down = true;
                        
                    }

                }
            }
        
        
        //check += 1;
        //Debug.Log(check);
        /*if (check == 2)
        {
            Debug.Log("check");
            if (other.gameObject.name == "water")
            {
                Debug.Log("in here");
                planky_move += new Vector3(0, -1, 0);
                //transform.position -= new Vector3(0, 1, 0);
            }
            transform.position += planky_move;
        }
        */
	}

}