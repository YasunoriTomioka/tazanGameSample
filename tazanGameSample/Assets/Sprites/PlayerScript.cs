using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rope;
    public Transform ropeParent;

    public LineRenderer line = null;
    GameObject clone;

    int TouchCount;
    int LineCount;

    int ropeSpeed = 30;
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))　//画面をタップしたら
        {
            TouchCount +=1;
            LineCount =1;
            if(TouchCount ==1)
            {
                if(ropeParent.GetChild(0) != null)
                {
                     //ropeParentの子要素を削除→次のロープを生成するためすでに生成されてるロープを削除
                    GameObject obj = ropeParent.GetChild(0).gameObject;
                    Destroy(obj);
                    this.line.SetPosition(0,new Vector3(0,0,0));
                    this.line.SetPosition(1,new Vector3(0,0,0));
                }
            }
           
            
            if(TouchCount==2)
            {
                // ロープを生成して、ropeParentの子要素としてropeを入れる。clone は生成されたrope。
                clone = Instantiate(rope, new Vector3(this.transform.position.x+0.5f,this.transform.position.y+0.5f,12), Quaternion.identity,ropeParent); 
    
                // クリックした座標の取得（スクリーン座標からワールド座標に変換）
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    
                // 向きの生成（Z成分の除去と正規化）
                Vector3 shotForward = Vector3.Scale((mouseWorldPos - transform.position), new Vector3(1, 1, 0)).normalized;
    
                // 弾に速度を与える→ropeSpeedの値でropeの速さ確定
                clone.GetComponent<Rigidbody>().velocity = shotForward * ropeSpeed;

                

                TouchCount=0;

            }
 
        }

        if(TouchCount==0)  //Lineをひく条件式
        {

            if(LineCount == 1 && clone != null)　　//初期状態以降の線が引かれてる状態
            {
                this.line.SetPosition(0, this.transform.position);　//プレイヤーのポジションが始点
                this.line.SetPosition(1, clone.transform.position);　//cloneのポジションが終点
            }
            if(LineCount==0)　　　　　　　　　　　　　//初期状態
            {
                this.line.SetPosition(0, this.transform.position);
                this.line.SetPosition(1, ropeParent.GetChild(0).position);　
            }
            if(LineCount == 1 && clone == null)　　//振り子状態が終わった状態で線を消す
            {
                this.line.SetPosition(0,new Vector3(0,0,0));
                this.line.SetPosition(1,new Vector3(0,0,0));
            }
            
            
        } 

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "rope")
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }

    }

}
