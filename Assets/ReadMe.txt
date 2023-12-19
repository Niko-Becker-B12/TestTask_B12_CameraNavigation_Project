Author: Hendrik Park 
Letzte �nderung 19.12.23

ReadMe Navigation Tool: 

F�r eiene einwandfreie Benutzung soll das Tool im Play-Modus verwendet werden. Es kann aber auch ohne Play-Modus eingesetzt werden.  

Das Navigation Tool l�sst sich in Unity �ber Window -> Navigation Tool �ffnen. 

Mit "Add Camera" l�sst sich eine neue Kamera in der Scene als Kind von "Camera-Container" platzieren. 
Zus�tzlich dazu entsteht im Ingame-UI ein neuer Button f�r die jeweilige Kamera. 
Mit Hilfe dieser Buttons kann der Benutzer zwischen diesen Einstellungen hin und her fahren. 
Dabei werden von der Kamera die Einstellung position, rotation und field of view erfasst. 
Andere Einstellungen werden derzeit nicht animiert und auch nicht gespeichert. Sie lie�en sich aber leicht implementieren 

Mit "Save Camera Set" werden die Einstellungen des ausgew�hlten Scriptable Objects Camera Set �berschrieben und gespeichert. 

Mit "Load Camera Set" kann ein neues Camera Set geladen werden. F�ge dazu einfach ein anderes Scriptable Object unter "Camera Set" ein. 

Mit "Save as New Camera Set" k�nnen die aktuellen Kamera-Einstellungen als neues Scriptable Object mit neuem Namen abgespeichert werden. 
Alle Scriptable Object Camera Sets werden unter "Assets/Data" abgespeichert. 

Mit "Clear Camera Set" werden alle aktuellen Kameras in der Szene entfernt. 


Unter "JSON Filename" kann ein Name f�r das Abspeichern des Camera Sets als JSON-File festgelegt werden. 
Mit "Save current camera set as JSON" wird ein JSON-File unter "Assets/Data/JSON" abgespeichert. 
Mit "Load camera set from JSON" kann eine JSON-Datei ausgelesen und als camera set in der Szene geladen werden.
Der Name des JSON-Files muss hier �ber JSON-Filename eingegeben werden. 



