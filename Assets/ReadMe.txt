Author: Hendrik Park 
Letzte Änderung 19.12.23

ReadMe Navigation Tool: 

Für eiene einwandfreie Benutzung soll das Tool im Play-Modus verwendet werden. Es kann aber auch ohne Play-Modus eingesetzt werden.  

Das Navigation Tool lässt sich in Unity über Window -> Navigation Tool öffnen. 

Mit "Add Camera" lässt sich eine neue Kamera in der Scene als Kind von "Camera-Container" platzieren. 
Zusätzlich dazu entsteht im Ingame-UI ein neuer Button für die jeweilige Kamera. 
Mit Hilfe dieser Buttons kann der Benutzer zwischen diesen Einstellungen hin und her fahren. 
Dabei werden von der Kamera die Einstellung position, rotation und field of view erfasst. 
Andere Einstellungen werden derzeit nicht animiert und auch nicht gespeichert. Sie ließen sich aber leicht implementieren 

Mit "Save Camera Set" werden die Einstellungen des ausgewählten Scriptable Objects Camera Set überschrieben und gespeichert. 

Mit "Load Camera Set" kann ein neues Camera Set geladen werden. Füge dazu einfach ein anderes Scriptable Object unter "Camera Set" ein. 

Mit "Save as New Camera Set" können die aktuellen Kamera-Einstellungen als neues Scriptable Object mit neuem Namen abgespeichert werden. 
Alle Scriptable Object Camera Sets werden unter "Assets/Data" abgespeichert. 

Mit "Clear Camera Set" werden alle aktuellen Kameras in der Szene entfernt. 


Unter "JSON Filename" kann ein Name für das Abspeichern des Camera Sets als JSON-File festgelegt werden. 
Mit "Save current camera set as JSON" wird ein JSON-File unter "Assets/Data/JSON" abgespeichert. 
Mit "Load camera set from JSON" kann eine JSON-Datei ausgelesen und als camera set in der Szene geladen werden.
Der Name des JSON-Files muss hier über JSON-Filename eingegeben werden. 



