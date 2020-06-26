using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScript : MonoBehaviour
{
    // Start is called before the first frame update
    HingeJoint hig;
  
    Rigidbody PlayerRb;
    Rigidbody RopeRb;

    JointLimits Jlim; 
    

    void Start()
    {
        PlayerRb = GameObject.Find("Player").GetComponent<Rigidbody>();

    // Update is called once per frame
    }
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "rope")  //ステージにロープのキューブがあったたら
        {
            collision.gameObject.AddComponent<HingeJoint>();  　　//ロープにHingeJointを追加
            hig = collision.gameObject.GetComponent<HingeJoint>();  //さっそくつけたHingeJointを取得
　　　　　　　RopeRb = collision.gameObject.GetComponent<Rigidbody>();　//ロープのrigidbodyを取得
            RopeRb.isKinematic =true;　　　　　　　　　　　　　　　　　　　　//ロープを固定
            hig.connectedBody=PlayerRb;　　　　　　　　　　　　　　　　　　　//プレイヤーのrigidbodyをHingejointのconnectedbodyに代入
            hig.axis = new Vector3(0,0,1);　                        　//HingeJointの回転方向をして
            　　　　　　
        }
    }
}
