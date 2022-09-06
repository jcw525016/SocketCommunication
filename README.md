# SocketCommunication

After launching Server, please follow the steps below. <br>

Step 1 : [Server] <br>
Click "Start Server". <br>
![image](https://user-images.githubusercontent.com/59195820/188651906-b984d08c-164e-4bfe-9165-685c8165ca30.png)<br>

Step 2 : [Server] <br>
After succesfully starting Server, you can see the message "Waiting for a connection..."<br>
![image](https://user-images.githubusercontent.com/59195820/188652122-3ec89a24-be55-4ddb-a813-ce710f65244f.png)<br>

Setp 3 : [Client] <br>
First, fill in "Requested File" and "File Save Path".<br>
![image](https://user-images.githubusercontent.com/59195820/188652959-7b9f6f0a-5328-44c9-b6db-1c246c3db7d7.png)<br>

Setp 4 : [Client] <br>
You can use "Browse" to select "File Save Path". <br>
![image](https://user-images.githubusercontent.com/59195820/188653204-cfc850dd-4245-454a-b877-af6d3740f9e9.png)<br>

Setp 5 : [Client] <br>
After filling "Requested File" and "File Save Path", click "File Download" to request Server to send back required file.<br>
![image](https://user-images.githubusercontent.com/59195820/188653424-c17dac26-73db-4874-84a4-137aa323e263.png)<br>

Setp 6 : [Client] <br>
If Server responds succefully, "ServerIP" and "ServerPort" would be updated.<br>
If the file exists in Server, Server will send back requested file to client and save file under "File Save Path".<br>
You can see the message "File download success !!".<br>
![image](https://user-images.githubusercontent.com/59195820/188653710-8b5f29e0-dfdc-44a4-a64d-a3b077c40fdf.png)<br>

Setp 7 : [Client] <br>
You can see the requested file - "test.txt" from "File Save Path" <br>
![image](https://user-images.githubusercontent.com/59195820/188653864-1ba0da24-efda-406c-9382-fb6382833658.png)<br>

Setp 8 : [Server] <br>
You can see the Server log for incoming request.<br>
![image](https://user-images.githubusercontent.com/59195820/188653955-d56522e0-9326-4132-a887-0621972798d5.png)<br>

Setp 9 : [Client] <br>
If the requested file does not exist in Server, Server responds ERROR message to Client.<br>
![image](https://user-images.githubusercontent.com/59195820/188654298-f834aa9a-5275-4cac-8c9c-32306ed2f14e.png)<br>

Setp 10 : [Server] <br>
If the requested file does not exist in Server, Server would show error message in the log. <br>
![image](https://user-images.githubusercontent.com/59195820/188654386-cff449df-f8f0-42d9-bd19-227800ab895d.png)<br>
