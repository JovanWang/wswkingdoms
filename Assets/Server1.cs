using UnityEngine;  
using System.Collections;  
using System.Net.Sockets;  
using System.IO;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Net;
using System.Text;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public class Server1 : MonoBehaviour
{  
	readonly string _ip = "127.0.0.1"; //
	readonly int _port = 3563;
    private MqttClient mqttClient;

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 100), "Send Public IP"))
        {
            Debug.Log("678");
            //第一个参数是ip地址，第二个参数是端口地址
            mqttClient = new MqttClient(IPAddress.Parse(_ip),_port,false, new X509Certificate(), MqttSslProtocols.None);
            Debug.Log("6789");
            // 消息接收事件
            mqttClient.MqttMsgPublishReceived += msgReceived;
            // 连接                                                                                                                                             
            mqttClient.Connect("Client ID", "admin", "password");
            // 发送登录消息
            mqttClient.Publish("Login/HD_Login/1", Encoding.UTF8.GetBytes("{\"userName\": \"username\",\"passWord\": \"Hello,anyone!\"}"));

		}  
	}
    static void msgReceived(object sender, MqttMsgPublishEventArgs e)
    {
        Debug.Log("服务器返回数据");
        string msg = System.Text.Encoding.Default.GetString(e.Message);
        Debug.Log(msg);
    }
 
    // Update is called once per frame  
    //void Update()
    //{

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        //这个字符串是向服务器发送的数据信息  
    //        string strValue = "123";
    //        // 发送一个内容是123 字段是klabs的信息  
    //        mqttClient.Publish("klabs", Encoding.UTF8.GetBytes(strValue));
    //        Debug.Log("发送数据123");
    //    }
    //}

}  