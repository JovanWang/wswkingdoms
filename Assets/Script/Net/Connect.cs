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

public class Connect
{

    private static Connect connect;

    private Connect()
    {
    }
    public static Connect GetInstance()
    {
        if (connect == null)
        {
            connect = new Connect();
        }
        return connect;
    }
    readonly string _ip = "127.0.0.1"; //
    readonly int _port = 3563;
    private MqttClient mqttClient;

    public void ConnectServer()
    {
        mqttClient = new MqttClient(IPAddress.Parse(_ip), _port, false, new X509Certificate(), MqttSslProtocols.None);
        mqttClient.MqttMsgPublishReceived += connect.msgReceived;
        mqttClient.Connect("Client ID", "admin", "password");
        mqttClient.Publish("Login/HD_Login/1", Encoding.UTF8.GetBytes("{\"userName\": \"username\",\"passWord\": \"Hello,anyone!\"}"));
    }

    void msgReceived(object sender, MqttMsgPublishEventArgs e)
    {
        Debug.Log("服务器返回数据");
        string msg = System.Text.Encoding.Default.GetString(e.Message);
        Debug.Log(msg);
    }

}

