#VR Giant
[TOC]

##Einleitung
Das hier beschriebene Spiel entstand im Rahmen eines Projektes in den Hochschulmodulen "Entwicklung von Multimediasystemen" und "Computergrafik" an der Hochschule für Technik und Wirtschaft Berlin. Die daran beteiligten Studenten waren *R.Schlett*, *R.Wegner-Repke* und *P.Bönsch*.

##Abstract
VR Giants ist ein Virtual Reality Spiel, welches exklusiv für die HTC Vive mittels des Unity SteamVR Plugins entwickelt wurde. Das Spiel fällt in das Genre **Tower Defense**[^td]. Mittels [Vive Controller](https://www.vive.com/us/accessory/controller/) kann sich der Spieler in der Welt bewegen, neue Türme in der Spielwelt platzieren und bereits platzierte Türme betreten. Auf Türmen können offensive Fähigkeiten gewirkt werden, um die Wellen von Gegnern an dem Erreichen der eigenen Basis zu hindern.

##Anforderungen
Folgende Anforderungen wurden für das Modul *Entwicklung von Multimediasystemen* festgelegt.

**Spielfeld**

Das Spielfeld soll ein von Bergen umrandete Tal sein, durch welches sich ein Pfad zieht. Gegner sollen dem Pfad durch das Spielfeld folgen, bis sie am Endpunkt des Pfades terminieren. Am Ende des Pfades soll sich die Basis des Spielers befinden.

**Spieler**

Der Spieler soll sich in einer erhöhten Position im Spielfeld befinden. Dies soll einen besseren Überblick über das Spielfeld ermöglichen und erlauben sich noch besser in die Sichtweise eines Giganten zu versetzen.
Als ein **Gigant** soll sich der Spieler frei im Tal bewegen können. Des Weiteren sollen Türme auf einer festgelegten Fläche im Tal platziert werden können. Ebenso soll das interagieren mit den einzelnen Türmen möglich sein. Dabei soll der Spieler **Türme betreten** können um auf diesen offensive Tätigkeiten zu wirken.
Darunter fallen das Werfen von explosiven Wurfgeschossen auf dem Brickboy Tower (Grenadier) und das Abfeuern von Pfeilen mit einem Bogen auf dem Archer Tower (Bogenschütze). Die Wurfgeschosse des Brickboy Towers sollen im Gegensatz zum Archer Tower bei Detonation auf dem Boden einen Flächenschaden (auch Area of Effect) verursachen. Der geschossene Pfeil des Bogenschützen soll einen Einzelschaden am eingetroffenen Gegner (Single Target) verursachen.

**Turmarten**

Unter die zu platzierende Türme sollen der Bogenschützenturm (Archer Tower) und der Grenadier Turm (Brickboy Tower) fallen. Die Türme sollen sich im Aussehen voneinander unterscheiden.

**Spielprinzip**

Aufgabe des Spielers in VR Giants soll es sein mit Hilfe der verschiedenen Turmtypen unterschiedliche Gegnertypen, welche in mehreren Wellen auftreten, zu bezwingen. Dabei soll eine Gegnerwelle immer aus einer beliebigen Anzahl verschiedener Gegnertypen bestehen. Erst nachdem eine Welle vollkommen bewältigt wurde soll es zum erscheinen der nächsten Welle kommen. Erreicht ein Gegner das Ende des Pfades und somit die Spielerbasis, soll das Leben des Gegners anteilig den Lebenspunkten der Spielerbasis abgezogen werden.
Das Spiel soll enden, wenn der Spieler kein Leben mehr hat oder alle Gegnerwellen besiegt wurden.


##Verwendete Technologien


Für die Umsetzung des Projektes wurden folgende Technologien eingesetzt:

- HTC Vive (2 Motion Controller, 2 Lighthouse Tracker, Virtual Reality Head Mounted Display)
- Desktop-Computer (CPU: Intel 2500k ~4,5 GHz, GPU: GTX 980TI, 16GB RAM)
- wurde die ersten vier Wochen eingesetzt und später von dem Alienware Laptop der HTW Berlin abgelöst 
- Alienware Laptop (GPU: GTX 1060) 

##Systemabbild
![Systemabbild][systemabbild]
[systemabbild]: http://i.imgur.com/LcIVZK7.png "Systemabbild"

##Spiel
**Spielwelt**

Das Spielfeld ist konzeptionell als große Fläche (Tal) gestaltet, welche von Bergen begrenzt wird. Durch das Tal zieht sich ein Pfad in einer geschwungenen Art. Auf dem Pfad sind für den Spieler nicht sichtbare Wegpunkte gesetzt. Diesen Wegpunkten folgen die Gegner bis sie in einem Endpunkt terminieren. Innerhalb des Tals gibt es einen limitierten Spielbereich. Auf der nicht zum Pfad gehörenden Fläche des Tals sind verschiedenfarbige Bäume und Steine als Gestaltungselemente der Spielwelt platziert.  
Am Ende des Spielbereichs befindet sich die Spielerbasis, welche das Ziel der Gegner darstellt.

**Turmarten**

Der Spieler hat die Möglichkeit entlang des Pfades drei verschiedene Arten von Türmen - *Archer Tower*, *Brickboy Tower* und *Mage Tower* - zu platzieren. Dafür existieren links und rechts neben dem Pfad jeweils eine markierte Fläche (Place Area), welche in etwa die Hälfte der Breite des Pfades haben. Im Laufe des Spieles kann der Spieler maximal sechs Türme auf die Place Area setzen, jeweils zwei von jedem Turmtyp.

Alle Türme verfügen über einen Autoattack der ein Projektil in Flugrichtung des Gegners startet. Beim Auftreffen des Projektils werden dem Gegner Lebenspunkte abgezogen. Die Schadenspunkte, welche durch einen Autoattack initiiert werden fallen geringer aus, als die einer Spielerattacken.
Abhängig von der Turmart ermöglicht der Turm dem Spieler in drei verschiedene Spielerrollen - *Archer*, *Brickboy*, *Mage* - zu wechseln. Dies geschieht mit dem Betreten eines Turms. 

Die **Archer** Rolle bietet dem Spieler die Funktionalität mit einem Bogen und zyklisch neu erstellten Pfeilen dem Gegner Lebenspunkte abzuziehen. Damit ein Spieler die Gegnerwellen nicht mit einem Pfeilregen überfallen kann, wurde ein Delay[^delay] von einer Sekunde für das Spawnen[^spawnen] der Pfeile festgelegt. Ein Pfeil bleibt nach dem Abschuss in seinem Ziel für wenige Sekunden stecken und wird anschließend aus dem Spiel entfernt.

Bei der **Brickboy** Rolle handelt es sich um eine Art Grenadier bzw. eine durch die Alchemie im Mittelalter entdeckte Art des [Schwarzpulver- und Explosionshandwerks](https://dasmittelalterderblog.com/category/waffen-des-mittelalters/). Die Aufgabe dieser Rolle ist es explosive Ziegelsteine auf die anstürmenden Schaaren von Gegnern zu werfen. Dafür existiert auf dem Turm eine *Brick Chest*, (eine Truhe in welcher die Ziegel aufbewahrt werden) welche zyklisch neue *Exploding Bricks* erstellt (Intervall von einer Sekunde, maximal drei pro Truhe). Diese können vom Spieler aufgehoben und auf die Spielwelt geworfen werden. Dabei entsteht eine Explosion, die Gegnern in einem gewissen Radius der Explosion Lebenspunkte abzieht.  

In der **Mage** Rolle besitzt der Spieler die Möglichkeit drei verschiedene Zauber zu wirken. Dafür muss der Spieler in seinem Sichtbereich mit einem Controller eine geometrische Form zeichnen (siehe Steuerung Mage State). Unter die zur möglichen Zauber fallen *der Frostzauber*, *der Feuerzauber* und *der Verwirrungszauber*.

Der *Frostzauber* wird durch das Zeichnen eines Dreiecks heraufbeschworen. Er wird visualisiert durch einen blau-weißen Eisball. Mit ihm wird es dem Spieler ermöglicht, Gegnern in einem festgelegten Radius, einen leichten Schaden zuzufügen und diese zusätzlich für einen Zeitraum von fünf Sekunden in ihrer Bewegung einzuschränken (Bewegungsgeschwindigkeit wird verringert). 

Mit dem Zeichnen eines Kreises wird ein *Feuerzauber* erstellt. Dieser gewährt dem Spieler das Herbeiführen eines orangefarbenen Feuerballs. Der aus einem Feuerball resultierende Flächenschaden (Area of Effect) entsteht beim Kontakt mit einem Gegner oder der Spielwelt (dabei müssen sich Gegner im Radius des Feuerzaubers befinden).

Bei einer Verzauberung mit dem *Verwirrungszauber* setzen Gegner ihr neues Wegziel zwei Wegpunkte zurück aufgrund von Desorientierung. Dies resultiert in einer Richtungsänderung. Der Spieler hat anschließend mehr Zeit dem Gegner auf seiner aktuellen Position Schaden zu verursachen, ohne dass dieser einen Wegeforschritt machen kann. Der Verwirrungszauber wird durch das Zeichnen eines Vierecks heraufbeschworen und durch einen hellgrün-beigefarbenen Magieball visualisiert.

Sämtliche Zauber befinden sich nach dem Heraufbeschwören in Form eines Magieballs über dem Controller mit dem sie erstellt wurden. Anschließendes Wirken des Zaubers führt dazu, dass die Magiebälle in "Zeigrichtung" des Controllers fliegen und nach der ersten Kollision mit einem Element der Spielwelt explodieren.

Mehr Informationen zum Magier unter "Separation - Computer Grafik - Spieler".

**Basis**

Die *Basis* ist das Hauptgebäude des Spielers, welche es zu verteidigen gilt. Auf den vorderen beiden Türmen der Spielerbasis befinden sich wie auf dem Brickboy Tower *BrickChest's*, welche zur Verteidigung der Basis genutzt werden können. Ebenso verfügen die ersten beiden Türme der Basis über einen Autoattack. Erreichen Gegner die Basis lösen diese sich in einer Rauchwolke auf und ziehen der Basis Lebenspunkte ab. Fallen die Lebenspunkte der Basis auf null, geht diese in einem Feuerwerk auf. Der Spieler hat dann das Spiel verloren.

**Gegner**

Der Spieler muss sich im Verlauf des Spiels gegen Wellen[^enemywave] von Gegnern behaupten. Ziel der Gegner ist es dem Pfad entlang bis zur Basis zu folgen um dieser Lebenspunkte abzuziehen. Die Gegner sind unterteilt in drei verschiedene Basis-Gegnertypen - *Bogenschütze*, *Magier* und *Krieger* - und einen *Bossgegner* (Pirat).
Die drei Basis-Gegnertypen sind zusätzlich in die Klassen *Normal-Enemy* und *Mid-Enemy* untergliedert. Einzige Unterschiede dieser Klassen ist die erhöhten Lebenspunkte und die Vergrößerung der "Mid-Enemies".
Fallen die Lebenspunkte eines Gegners durch das Eintreffen eines Projektils oder durch Angriff-Aktionen des Spielers auf null, lösen sich die Gegner in einer Rauchwolke auf.

Ebenfalls wurde für eine bessere Spielbalance ein Trinity-System implementiert. Hierzu finden Sie Informationen unter Separation - Computer-Grafik - Spielprinzip.

**Schadensmodell**

Das Schadensmodell im Spiel ist über eine Einfärbung der Spielelemente realisiert. Das heißt, dass sich relativ zu den noch vorhandenen Lebenspunkte die Farbe der Elemente ändert. Hierfür wurde ein Farbverlauf von grün zu rot verwendet, wobei grün für viel bzw. volles Leben und rot für wenig Leben steht. Dieses Schadensmodell findet sowohl bei den Gegnern als auch bei der Spielerbasis Anwendung.

**Ziel**

Ziel des Spiels ist es, möglichst schnell alle Gegner vom Ankommen in der Basis abzuhalten, sprich alle Gegnerwellen zu überstehen, ohne dabei die Lebenspunkte der *Basis* auf null sinken zu lassen. Nach dem Beenden des Spiels gelangt der Spieler in eine Sieg-Scene[^scene] oder eine Niederlage-Scene. In dieser wird ihm sein Spielresultat visualisiert und die benötigte Zeit sowie die gewirkten Schadenspunkte angezeigt. 

###Steuerung
Im nachfolgenden Text wird die Steuerung des Spiels erklärt.

####Giant State

Dieser State wird automatisch beim Betreten oder Verlassen eines Turms de- bzw. aktiviert.

**Move State (Giant)**

Der Spieler ist in der Lage, sich innerhalb des zugelassenen Bereichs zu teleportieren. Dies wird ermöglicht durch das Drücken des Trackpads im vorderen Bereich und das anschließende Anvisieren der gewünschten Position. Die Möglichkeit der Teleportation wird visualisiert durch das Aufleuchten eines blauen Lasers und einer weißen Markierung auf dem Boden. Sollte der Spieler eine Fläche außerhalb des zugelassenen Bereichs fokussieren, so wird dies durch einen rötlich gefärbten Laser veranschaulicht.

**Place State**

Um in den Place State zu gelangen, muss der Spieler den Trigger auf einem der Controller gedrückt halten. Nun ist er in der Lage einen Turm zu platzieren. Zur Auswahl stehen die oben bereits genannten Türme, die nun über ein zusätzliches Drücken des Trackpads in die linke (Archer Tower), rechte (Mage Tower) oder obere (Brickboy Tower) Richtung ausgewählt werden können. Beim Loslassen des Triggers wird nun ein Turm platziert. Zu beachten ist, dass die Anzahl der Türme auf zwei Stück pro "Turm - Art" begrenzt ist, jedoch beliebig oft neu platziert werden können. Sollte der Spieler doch keinen Turm stellen wollen, so kann der Vorgang über das Drücken des unteren Bereichs des Trackpads abgebrochen werden. Der Place State kann über das Loslassen des Triggers wieder verlassen werden.

**Menu State**

Den Menu State kann man erreichen, indem der “Menu - Button” auf einem der Controller gedrückt wird. Hier kann der Spieler entweder das Tutorial erneut öffnen, das Spiel neu starten oder es vollständig beenden. Das Menü kann kann durch ein erneutes Drücken auf einen der “Menu - Buttons” (jeweils einer auf jedem Controller) geschlossen werden.

**Tutorial State**

Der Tutorial State ist der erste State in den der Spieler nach dem Starten des Spiels gelangt. Hier kann er sich eine Einsteigeranleitung durchlesen, um sich mit allen relevanten Funktionen der Steuerung vertraut zu machen. Auch hier ermöglicht das Trackpad dem Spieler, durch die Optionen zu navigieren oder das Tutorial zu beenden.

####Tower State

Dieser State wird erreicht, indem sich der Spieler im Move State (Giant) auf einen Turm oder seine Burg teleportiert (gekennzeichnet durch einen grünen Laser). Der Tower State kann durch das Drücken des Trackpads im unteren Bereich wieder verlassen werden.

**Move State (Tower)**

Dieser State wird automatisch aktiviert, sobald der Spieler den Turm betritt. Er ist nun ebenfalls in der Lage, sich auf dem Turm zu bewegen, wie auch schon aus dem vorherigen Move State (Giant) bekannt. Hierbei ist die Fläche, auf der er sich bewegen kann, auf den Turm begrenzt. Deaktiviert wird dieser State, sobald der Spieler den Turm wieder verlässt.

**Brickboy State**

In diesem State ist der Spieler in der Lage, die roten Ziegelsteine aus der, auf dem Turm platzierten Kiste, durch das Gedrückthalten des Triggers aufzunehmen und wieder loszulassen. Wirft der Spieler einen dieser auf einen Bereich außerhalb des Turms, so entsteht eine Explosion, die Gegnern in einem Umkreis Schaden zufügt.

**Archer State**

Im Archer State bekommt der Spieler automatisch einen Bogen und einen Pfeil, der sich in den Bogen, mit Hilfe des Triggers, einspannen lässt. Wird der Trigger losgelassen, so wird der Pfeil abgeschossen. Trifft dieser auf dem Weg seiner Flugbahn einen Gegner, so fügt er diesem Schaden zu.

**Mage State**

Dieser State ermöglicht es dem Spieler, mächtige Zauber zu wirken. Hält der Spieler den Trigger eines Controllers gedrückt, so ist er in der Lage, eine der folgenden geometrischen Formen in der Luft und in seinem Sichtbereich zu zeichnen:

Kreis: Feuerzauber 
* fünf Schadenspunkte, großer Radius
Dreieck: Frostzauber 
* Gegner wird für fünf Sekunden um die Hälfte langsamer, mittlerer Radius
 Viereck: Verwirrungszauber 
* Gegner ist verwirrt und geht zwei Wegpunkte zurück, kleiner Radius

###Abgrenzung
####Computer Grafik
Folgende Funktionalitäten wurden für das Modul *Computer Grafik* umgesetzt.

**Spielwelt**

Die Spielwelt wurde komplett in in einer Low Poly Optik umgesetzt. Darunter fallen das Terrain, die Türme, die Spielerbasis (Gebäude am Ende des Pfades), die Gegner (vier Gegnertypen), die explosiven Steine, Bäume, sowie Steine und der Gegner Spawn (Höhle). Jedes dieser Objekte wurde in Blender gemodelt und mit eigenem Material versehen um Flächen der Objekte zu färben. 

**Gegner**

Die ursprünglichen Gegnertypen (Sphere, Würfel und Ellipse) wurden durch die drei Gegnerklassen Krieger, Bogenschütze und Magier ersetzt. Diese haben jeweils die Basiseigenschaften Leben, Bewegungsgeschwindigkeit und einen Flag der angibt, ob sie verwirrt sind.

Die Gegnertypen werden zusätzlich in kleine Gegner und mittlere Gegner unterschieden. Dabei ist zu erwähnen, dass kleine Gegner zehn Lebenspunkte besitzen und mittlere Gegner 30. Zusätzlich existiert noch ein Bossgegner, welcher 100 Lebenspunkte besitzt.

**Spieler**

Zu den bereits vorhandenen Spielerrollen Bogenschützenturm und “Explosions”-Turm wurde die Rolle Magierturm hinzugefügt. Diese umfasst das wirken von Zaubern, welches bereits in der Veranstaltung Computergrafik genauer demonstriert wurde.
Implementiert wurde hier eine Sketch Recognition, welche es ermöglicht durch das Zeichnen von geometrischen Formen (Kreis, Dreieck, Viereck) im Sichtfeld des Spielers, Zauber heraufzubeschwören.

[Gesture Detection Utility](https://bitbucket.org/aradar/mm_vr_giants/src/42f39b76eab6288e4c5842da67f0820487abd681/Assets/Scripts/Utils/GestureDetectionUtility.cs?at=dev-cg&fileviewer=file-view-default)

[Spellcast Detection](https://bitbucket.org/aradar/mm_vr_giants/src/42f39b76eab6288e4c5842da67f0820487abd681/Assets/Scripts/Behaviour/SpellCastDetectionBehaviour.cs?at=dev-cg&fileviewer=file-view-default)

**Benutzeroberfläche**

Es wurde ein UI implementiert mit dem man ein Tutorial öffnen kann, in dem die grundlegenden Spielmechaniken erneut erklärt werden. Des Weiteren kann man über das UI das Spiel neu starten oder beenden.

**Spielprinzip**

Um ein besseres einbeziehen der Spielerrollen - Magierturm, Bogenschützenturm, “Explosions”-Turm - zu erzielen wurde zusätzlich ein Trinity-System zwischen den Spielerrollen und den Gegnertypen implementiert. Grundlegend macht jeder Spieler beim wirken eines Angriffs standardmäßig auf einem Turm einen Schaden von fünf Lebenspunkten. Diese verdoppeln sich jedoch, wenn die aktuelle Spielerrolle effektiv gegen den angegriffenen Gegnertyp ist (zehn Schadenspunkte), bzw. halbiert sich wenn die aktuelle Spielerrolle ineffektiv gegen den angegriffenen Gegnertyp ist (2.5 Schadenspunkte).
Die Spielerrollen-Gegnertypen-Trinity ist folgendermaßen aufgebaut:

![Schema des Trinity - Systems][logo]
[logo]: http://i.imgur.com/qyxfrlG.png?1 "Schema des Trinity - Systems"

Gegen die eigene Klasse macht der Spieler den Standardschaden von fünf.

Abschließend wurde noch zwei End-Scenes zum Spiel hinzugefügt, welche je nach Sieg oder Niederlage auftauchen. In diesen erhält der Spieler neben einer visuellen Veranschaulichung des Spielausgangs noch eine Information zu seinem gewirkten Schaden, der benötigten Zeit, sowie einen damage per second Wert. Die erzielten Ergebnisse werden dann eine Highscores.txt Datei im Spielverzeichnis angehängt.

##Gesamteinschätzung

Das erste Problem war, wie so oft, der Anfang. Denn allein eine Idee für ein Projekt zu bekommen, ist nicht einfach, besonders da man an allen Enden merkt, dass dies und jenes eventuell nicht funktioniert, vielleicht zu langweilig oder viel zu umfangreich ist. Wir konnten uns recht schnell auf eine zu verwendende Technologie einigen: Virtual Reality. Aber wie? Im Rahmen der Veranstaltung hatten wir Virtual Reality nur mit dem Google Cardboard kennengelernt, was ohne Frage vollkommen ausreicht, uns jedoch nicht spektakulär genug erschien, um damit ein ganzes Projekt zu füllen. Letztlich kamen wir auf die Idee, dies mit einer HTC Vive umzusetzen.
Nachdem wir die erste Hürde, die Beschaffung dieser Technologie, durch unsere Beharrlichkeit überwunden hatten, begann unser sehr sportliches Rennen gegen die Zeit. Vier Wochen blieben uns nun für die Umsetzung unserer Projektidee. Diese ursprünglich simple Idee wuchs mit der Zeit und jeder Funktion die wir realisierten. Die Zeitbegrenzung führte einerseits zu einem gewissen Grad an Stress, andererseits aber haben wir ihr einen schnellen Projektfortschritt zu verdanken. So folgte ein Erfolgserlebnis dem anderen und schweißte das Team zusammen.
Durch das doch recht rasante Voranschreiten des Projekts wurden wir optimistischer, auch den geplanten Teil für das Modul Computergrafik erfolgreich umsetzen zu können. Nachdem wir die erste HTC Vive wieder abgeben mussten, war ein Großteil des gesamten Projekts bereits fertiggestellt. Das daraus resultierende Problem war jedoch, dass wir keine Brille mehr hatten, um Weiterentwicklungen austesten zu können. Während der Zeit des Wartens begannen wir mit der visuellen Komplettüberholung unserer Spielwelt in Blender.
Als wir dann die neue HTC Vive zur Verfügung gestellt bekommen hatten, wurde das Projekt überarbeitet und mit vielen zusätzlichen und existenziellen Features versehen, die das Spiel letztlich erst spielbar und das Spielerlebnis für den Nutzer genießbar machten.
Während der gesamten Entwicklung stießen wir sowohl an mathematische, als auch hardwaretechnische Grenzen, die uns an der Umsetzung von spielerischen Elementen behinderten. Trotz Dessen entwickelten wir Möglichkeiten um diese zu umgehen und im Idealfall sogar zu verhindern. So war es beispielsweise nicht möglich, unser Spiel mit Schatten oder einem dichteren Wald auszustatten, da dies für manche Hardware zu rechenintensiv gewesen wäre. Auch hatten wir Ideen, die aus dem Rahmen der von Unity gedachten Konzeption des Entity-Component-Systems fielen und uns zwischenzeitlich stark zurück warfen.
Final ist zu sagen, dass Projekt in vollem Umfang erfolgreich umgesetzt wurde und wir mit dem schlussendlichen Resultat mehr als zufrieden sind. Eine bessere interdisziplinäre Zusammenarbeit, zwischen den Fachbereichen unserer Hochschule, wäre in unseren Augen jedoch noch wünschenswert gewesen.

##Selbsteinschätzungen

>Während des Projekts war eine der größten Herausforderungen für mich, den Sinn hinter dem von Unity umgesetzten Konzept zu verstehen, der sich mir erst nach vielen Stunden erschlossen hat. Die Schnittstelle zwischen dem Code und den damit verbundenen „GameObjects“ und deren Zusammenwirken, habe ich erst nach einiger Zeit nachvollziehen können.
Dennoch habe ich selbst durch das Projekt gute Einblicke in Unity und dessen Funktionsweise bekommen. Auch konnte ich viele Aspekte der Programmierung von Spielen besser nachempfinden, während ich schon auf der anderen Seite erkannte, dass Unity doch auch unglaublich kompliziert werden konnte. Des Weiteren war es für mich während des Projektes sehr interessant zu sehen, dass gewisse mathematische Vorkenntnisse aus der Schule doch noch ihre Anwendung gefunden haben. Ob es um das Verstehen von Bewegungen im dreidimensionalen Raum oder um Schnittwinkelberechnung und -interpretation bei der Gestenerkennung ging. Auch wenn Quaternions wahrscheinlich noch eine längere Einarbeitungszeit benötigen würden, um diese wirklich zu verstehen und einsetzen zu können, so habe ich doch während des Projektes so manch mathematisches Prinzip wiedererkennen können.
Außerdem war es bemerkenswert zu sehen, wie sehr diese Technologie noch am Anfang der Entwicklung steht. Auch wenn die HTC Vive schon um einiges besser ist, als ein Google Cardboard, so hat man doch gemerkt, dass gerade die Bildschirmauflösung der Brille doch noch sehr gering ist. Ein Smartphone mit einer 4K-Auflösung würde hierbei wohl deutlich besser abschneiden. Auf der anderen Seite ist es erstaunlich, wie präzise das Tracking der Brille und der Controller schon funktioniert und bin gespannt, was für Verbesserungen in den nächsten Jahren noch auf uns zukommen werden.

*~ Robin Wegner-Repke*

>Persönlich betrachtet war das Projekt für mich zeitlich sehr aufreibend und im Umfang für dieses Semester als sehr groß angesetzt. Dafür, dass ich zum ersten Mal mit Unity und auch mit einem Virtual Reality Head Mounted Display gearbeitet habe, lief die Einarbeitung und die anschließende Nutzung unproblematisch. Da ich bereits Vorkenntnisse in C# hatte, gelang mir das Arbeiten mit Unity auch bis auf weiteres nahtlos. Einzig das Entity-Component-System brachte mir und meinem Kommilitonen zyklisch neue Kopfschmerzen, da es viele bekannte und bewährte Entwurfsmuster aushebelt, sprich nicht nutzbar macht. Obwohl wir aufgrund der Knappen Zeit mitunter sehr gestresst an diesem Projekt gearbeitet haben muss ich sagen, dass ich sehr zufrieden mit dem Endresultat bin. Final kann ich sagen, dass ich voll hinter diesem Projekt stehe, da meinerseits viel Arbeit und Willenskraft eingeflossen ist und ich diese Anstrengungen ebenfalls von meinen Kommilitonen wahrgenommen habe. Ich hätte nicht gedacht, dass wir schlussendlich so ein sauberes Endprodukt entwickeln konnten da keiner von uns Vorkenntnisse in diesem Bereich hatte.

*~ Philipp Bönsch*

>Für mich persönlich stellte die Einarbeitung in das von Unity verwendete Entity-Component-System die größte Herausforderung dar, da sich dieses sehr stark vom Gedanken der Objektorientiertheit unterscheidet. Ebenso hat der Unity Editor mit seinem stellenweise sehr gewöhnungsbedürftigen Verhalten mich das eine oder andere mal die Stirn runzeln lassen.
In meinen Augen haben wir den Arbeitsaufwand gut zwischen uns drei aufteilen können. Hierzu hat die Tatsache, dass wir immer nur als Gruppe an dem Projekt arbeiten konnten stark beigetragen. Somit konnte jeder seine stärken einbringen und hatte die Möglichkeit sich in allen Teilbereichen zu verwirklichen. 
Durch dieses Projekt habe ich einen sehr schönen Einblick in die Entwicklung von Computerspiele wie auch den Umgang von VR-Headsets erlebt. Darüberhinaus konnte man viele Erfahrungen im Umgang mit 3D-Umgebungen sammeln. 
Zu guter Letzt sollte gesagt sein, dass es trotz des straffen Zeitplans ein sehr spaßiges und lehrreiches Projekt war und ich es jederzeit wieder durchführen würde.

*~ Ralph Schlett*


##Link zur ausführbaren Datei
https://goo.gl/FJBsKB

##Referenzen
[Simple FX - Cartoon Particles] 
(https://www.assetstore.unity3d.com/en/#!/content/67834)

[3D Modeling a Ranger’s Bow for VR]
(http://fusedvr.com/3d-modeling-a-rangers-bow-for-vr/)

[Building a Robin Hood VR Game (Unity VR Tutorial)]
(https://www.youtube.com/watch?v=Dh7Wwqs-s2c)

[Unity Documentation - Scripting API]
(https://docs.unity3d.com/ScriptReference)

[Sketch Recognition]
09-EMM_SBIM_Sketch_Recognition.pdf - aus dem Modul “Entwicklung Multimediasysteme”



##Glossar

[^td]: Das **Tower Defense** Genre ist ein Subgenre des [Echtzeit-Strategiespiels](https://www.techopedia.com/definition/1923/real-time-strategy-rts). Die Hauptaufgaben des Spieles bestehen darin verschiedene Arten von Türmen (Verteidigungsanlagen) aufzustellen. Diese sollen Wellen von Gegnern vor dem durchqueren der Karte und dem anschließendem zerstören der Spielerbasis (Gebäude am Ende des Pfades) hindern.

[^delay]: Ein **Delay** ist eine Verzögerung des Spielablaufs (meist im Sekunden / Millisekunden Bereich).

[^spawnen]: Unter **Spawnen** versteht man das Neuerstellen bzw. Neuauftauchen von Spielelementen.

[^enemywave]: Eine **Gegner Welle** ist Ansammlung von Gegnern, wobei diese in gleichmäßigen Zeitabständen aufeinanderfolgend beginnen dem Pfad  zu betreten.

[^scene]: Eine Art Level, welches nach Abschluss der aktuellen Szene optional geladen werden kann. 
