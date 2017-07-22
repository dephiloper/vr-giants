#VR Giant
[TOC]

##Einleitung
Das hier beschriebene Spiel entstand im Rahmen eines Projektes in den Hochschulmodulen "Entwicklung von Multimediasystemen" und "Computergrafik" an der Hochschule für Technik und Wirtschaft Berlin. Die daran beteiligten Studenten waren *R.Schlett*, *R.Wegner-Repke* und *P.Bönsch*.

##Abstract
VR Giants ist ein Virtual Reality Spiel, welches exklusiv für die HTC Vive mittels des Unity SteamVR Plugins entwickelt wurde. Das Spiel fällt in das Genre **Tower Defense**[^td]. Mittels [Vive Controller](https://www.vive.com/us/accessory/controller/) kann sich der Spieler in der Welt bewegen, neue Türme in der Spielwelt platzieren und bereits platzierte Türme betreten. Auf Türmen können offensive Fähigkeiten gewirkt werden, um die Wellen von Gegnern an dem Erreichen der eigenen Basis zu hindern.

##Anforderungen
###Entwicklung von Multimediasystemen
Folgende Anforderungen wurden für das Modul *Entwicklung von Multimediasystemen* festgelegt.

**Spielfeld**

Das Spielfeld ist konzeptionell als große Fläche (Tal) gestaltet, welche von Bergen begrenzt wird. Durch das Tal zieht sich ein Pfad in einer schlangenförmigen Art. Auf dem Pfad sind für den Spieler nicht sichtbare Wegpunkte gesetzt. Diesen Wegpunkten folgen die Gegner bis sie in einem Endpunkt terminieren. Innerhalb des Tals gibt es einen limitierten Spielbereich. 
Am Ende des Spielbereichs befindet sich die Spielerbasis, welche das Ziel der Gegner bildet.

**Spieler**

Der Spieler befindet sich auf einer erhöhten Position im Spielfeld. Dies ermöglicht einen besseren Überblick über das Spielfeld und erlaubt es noch besser die Sicht eines Giganten anzunehmen. Während des Spielverlaufes kann der Spieler in mehrere Stati wechseln, um auf verschiedene Türme zu wechseln und offensive Tätigkeiten zu wirken.

**Turmarten**

Unter die zu platzierende Türme fallen der Bogenschützenturm (Archer Tower) und der Grenadier Turm (BrickBoy Tower).

**States**

Die beiden Haupt-States sind der Giant State und der Tower State (auch On Tower State).

Im **Giant State** befindet sich der Spieler in einer erhöhten Position, was eine Art Draufsicht auf das Spiel ermöglicht (höher als Türme). Der Spieler kann sich in diesem State frei in der Spielwelt bewegen. Des Weiteren können Türme auf einer festgelegten Fläche in der Spielwelt hinzugefügt werden und das Teleportieren auf Türme ist möglich.
	
Im **On-Tower State** kann der Spieler offensive Aktionen wirken um die Gegnerwellen vor dem eintreffen in der Spielerbasis zu hindern. Darunter fallen das werfen von explosiven Wurfgeschossen auf dem BrickBoy Tower und das abfeuern von Pfeilen mit einem Bogen. Die Wurfgeschosse des BrickBoy Towers (Grenadier) verursachen im Gegensatz zum Archer Tower (Bogenschütze) bei Detonation auf dem Boden einen Flächenschaden (auch Area of Effect). Der geschossene Pfeil des Bogenschützen bewirkt einen Einzelschaden am eingetroffenen Gegner (Single Taget).

**Spielprinzip**

Der Spieler muss mit Hilfe der verschiedenen Turmtypen unterschiedliche Gegnertypen, welche in mehreren Wellen auftreten, bezwingen. Dabei besteht eine Gegnerwelle immer aus einer beliebigen Anzahl verschiedener Gegnertypen. Erst nachdem eine Welle vollkommen bewältigt wurde kommt es zum erscheinen der nächsten Welle. Erreicht ein Gegner das Ende des Pfades und somit die Spielerbasis, so verliert diese anteilig dem Leben des Gegners Lebenspunkte.
Das Spiel endet, wenn der Spieler kein Leben mehr hat oder alle Gegnerwellen besiegt wurden.

###Computer Grafik
Folgende Anforderungen wurden für das Modul *Computer Grafik* festgelegt.

**Spielwelt**

Die Spielwelt wurde komplett in in einer Low Poly Optik umgesetzt. Darunter fallen das Terrain, die Türme, die Spielerbasis (Gebäude am Ende des Pfades), die Gegner (4 Gegnertypen), die explosiven Steine, Bäume, sowie Steine und der Gegner Spawn (Höhle). Jedes dieser Objekte wurde in Blender gemodelt und mit eigenem Material versehen um Flächen der Objekte zu färben. 

**Gegner**

Die ursprünglichen Gegnertypen (Sphere, Würfel und Ellipse) wurden durch die 3 Gegnerklassen Krieger, Bogenschütze und Magier ersetzt. Diese haben jeweils die Basiseigenschaften Leben, Bewegungsgeschwindigkeit und einen Flag der angibt, ob sie verwirrt sind.

Die Gegnertypen werden zusätzlich in kleine Gegner und mittlere Gegner unterschieden. Dabei ist zu erwähnen, dass kleine Gegner 10 Lebenspunkte besitzen und mittlere Gegner 30. Zusätzlich existiert noch ein Bossgegner, welcher 100 Lebenspunkte besitzt.

**Spieler**

Zu den bereits vorhandenen Spielerrollen Bogenschützenturm und “Explosions”-Turm wurde die Rolle Magierturm hinzugefügt. Diese umfasst das wirken von Zaubern, welches bereits in der Veranstaltung Computergrafik genauer demonstriert wurde.
Implementiert wurde hier eine Sketch Recognition, welche es ermöglicht durch das Zeichnen von geometrischen Formen (Kreis, Dreieck, Viereck) im Sichtfeld des Spielers, Zauber heraufzubeschwören.

[Gesture Detection Utility](https://bitbucket.org/aradar/mm_vr_giants/src/42f39b76eab6288e4c5842da67f0820487abd681/Assets/Scripts/Utils/GestureDetectionUtility.cs?at=dev-cg&fileviewer=file-view-default)

[Spellcast Detection](https://bitbucket.org/aradar/mm_vr_giants/src/42f39b76eab6288e4c5842da67f0820487abd681/Assets/Scripts/Behaviour/SpellCastDetectionBehaviour.cs?at=dev-cg&fileviewer=file-view-default)

**Benutzeroberfläche**

Es wurde ein UI implementiert mit dem man ein Tutorial öffnen kann, in dem die grundlegenden Spielmechaniken erneut erklärt werden. Des Weiteren kann man über das UI das Spiel neu starten oder beenden.

**Spielprinzip**

Um ein besseres einbeziehen der Spielerrollen - Magierturm, Bogenschützenturm, “Explosions”-Turm - zu erzielen wurde zusätzlich ein Trinity-System zwischen den Spielerrollen und den Gegnertypen implementiert. Grundlegend macht jeder Spieler beim wirken eines Angriffs standardmäßig auf einem Turm einen Schaden von 5 Lebenspunkten. Diese verdoppeln sich jedoch, wenn die aktuelle Spielerrolle effektiv gegen den angegriffenen Gegnertyp ist (10 Schadenspunkte), bzw. halbiert sich wenn die aktuelle Spielerrolle ineffektiv gegen den angegriffenen Gegnertyp ist (2.5 Schadenspunkte).
Die Spielerrollen-Gegnertypen-Trinity ist folgendermaßen aufgebaut:

![Schema des Trinity - Systems][logo]
[logo]: http://i.imgur.com/qyxfrlG.png?1 "Schema des Trinity - Systems"

Gegen die eigene Klasse macht der Spieler den Standardschaden von 5.

Abschließend wurde noch zwei End-Scenes zum Spiel hinzugefügt, welche je nach Sieg oder Niederlage auftauchen. In diesen erhält der Spieler neben einer visuellen Veranschaulichung des Spielausgangs noch eine Information zu seinem gewirkten Schaden, der benötigten Zeit, sowie einen damage per second Wert. Die erzielten Ergebnisse werden dann eine Highscores.txt Datei im Spielverzeichnis angehängt.


##Verwendete Technologien
Für die Umsetzung des Projektes wurden folgende Technologien eingesetzt:

- HTC Vive (2 Motion Controller, 2 Lighthouse Tracker, VR-Brille)
- Desktop-Computer (CPU: Intel 2500k, GPU: 980TI, 16GB RAM)
- Wurde die ersten 4 Wochen eingesetzt und später von dem Alienware Laptop der HTW Berlin abgelöst 
- Alienware Laptop (GPU: 1060) 

##Systemabbild
![Systemabbild][systemabbild]
[systemabbild]: http://imgur.com/LcIVZK7 "Systemabbild"

##Spiel
**Spielwelt**

Die Welt ist ein von Bergen umrandetes Tal, durch welches sich ein geschwungener Pfad zieht. Am Ende des Pfades befindet sich die Basis des Spielers, in Form einer Burg.

**Türme**

Der Spieler hat die Möglichkeit entlang des Pfades 3 verschiedene Arten von Türmen - *Archer Tower*, *Brickboy Tower* und *Mage Tower* - zu platzieren. Alle Türme verfügen über einen Auto-Attack der ein Projektil in Flugrichtung der Gegner startet. Bei auftreffen des Projektils werden dem Gegner Lebenspunkte abgezogen.
Abhängig von der Turmart ermöglicht es der Turm in 3 verschiedene Spielerrollen - *Archer*, *Brickboy*, *Mage* zu wechseln. Dies geschieht beim betreten eines Turms.

 - Die **Archer** Rolle bietet dem Spieler die Funktionalität mit einem Bogen und zyklisch neu erstellten Pfeilen dem Gegner Lebenspunkte abzuziehen.
 - Bei der **Brickboy** Rolle existiert auf dem Turm eine *Brick Chest*, welche zyklisch neue *Exploding Bricks* erstellt. Diese können vom Spieler aufgehoben und auf die Spielwelt geworfen werden. Dabei entsteht eine Explosion, die Gegnern im Explosionsradius Lebenspunkte abzieht.  
 - In der **Mage** Rolle besitzt der Spieler die Fähigkeiten durch das Durchführen von Gesten 3 verschiedene Zauber zu wirken (*Eiszauber, Feuerzauber, Blitzzauber*).

**Basis**

Die *Basis* ist das Hauptgebäude des Spielers, welche es zu verteidigen gilt. Erreichen Gegner die Basis lösen diese sich in einer Rauchwolke auf und ziehen der Basis Lebenspunkte ab. Fallen die Lebenspunkte der Basis auf 0, geht diese in einem Feuerwerk auf. Der Spieler hat dann das Spiel verloren.

**Gegner**

Der Spieler muss sich im Verlauf des Spiels gegen Wellen[^enemywave] von Gegnern behaupten. Ziel der Gegner ist es dem Pfad entlang bis zur Basis zu folgen um dieser Lebenspunkte abzuziehen. Die Gegner sind unterteilt in 3 verschiedene Gegnertypen: *Normal Enemy*, *Fast Enemy* und *Resistance Enemy*.

 - Der *Resistance Enemy* hat eine prozentuale Resistenz gegen jegliche Art von Angriffen, was ihm eine längere Existenz ermöglicht.
 - Der *Fast Enemy* hat eine um Faktor x erhöhte Bewegungsgeschwindigkeit.
 - Der *Normal Enemy* besitzt weder eine erhöhte Bewegungsgeschwindigkeit, noch eine Resistenz gegen erlittene Schäden.

Fallen die Lebenspunkte eines Gegners durch das Eintreffen eines Projektils oder durch Angriff-Aktionen des Spielers auf 0, lösen sich die Gegner in einer Rauchwolke auf.

**Ziel**
Ziel des Spiels ist es möglichst alle Gegner von dem Ankommen in der Basis abzuhalten, sprich alle Gegnerwellen zu überstehen, ohne dabei die Lebenspunkte der *Basis* auf 0 sinken zu lassen.

###Steuerung

####Giant State

Dieser State wird automatisch beim Betreten oder Verlassen eines Turms de- bzw. aktiviert.

**Move State (Giant)**

Der Spieler ist in der Lage, sich innerhalb des zugelassenen Bereichs zu teleportieren. Dies wird ermöglicht durch das Drücken des Trackpads im vorderen Bereich und das anschließende Anvisieren der gewünschten Position. Die Möglichkeit der Teleportation wird visualisiert durch das Aufleuchten eines blauen Lasers und einer weißen Markierung auf dem Boden. Sollte der Spieler eine Fläche außerhalb des zugelassenen Bereichs fokussieren, so wird dies durch einen rötlich gefärbten Laser veranschaulicht.

**Place State**

Um in den Place State zu gelangen, muss der Spieler den Trigger auf einem der Controller gedrückt halten. Nun ist er in der Lage einen Turm zu platzieren. Zur Auswahl stehen die oben bereits genannten Türme, die nun über ein zusätzliches Drücken des Trackpads in die linke (Archer Tower), rechte (Mage Tower) oder obere (Brickboy Tower) Richtung ausgewählt werden können. Beim loslassen des Triggers wird nun ein Turm platziert. Zu beachten ist, dass die Anzahl der Türme auf zwei Stück pro "Turm - Art" begrenzt ist, jedoch beliebig oft neu platziert werden können. Sollte der Spieler doch keinen Turm stellen wollen, so kann der Vorgang über das Drücken des unteren Bereichs des Trackpads abgebrochen werden. Der Place State kann über das Loslassen des Triggers wieder verlassen werden.

**Menu State**

Den Menu State kann man erreichen, indem der “Menu - Button” auf einem der Controller gedrückt wird. Hier kann der Spieler entweder das Tutorial erneut öffnen, das Spiel neu starten oder es vollständig beenden. Das Menü kann kann durch ein erneutes Drücken auf einen der “Menu - Buttons” (jeweils einer auf jedem Controller) geschlossen werden.

**Tutorial State**
Der Tutorial State ist der erste State in den der Spieler nach dem Starten des Spiels gelangt. Hier kann er sich eine Einsteigeranleitung durchlesen, um sich mit allen relevanten Funktionen der Steuerung vertraut zu machen. Auch hier ermöglicht das Trackpad dem Spieler, durch die Optionen zu navigieren oder das Tutorial zu beenden.

####Tower State

Dieser State wird erreicht, indem sich der Spieler im Move State (Giant) auf einen Tower oder seine Burg teleportiert (gekennzeichnet durch einen grünen Laser). Der Tower State kann durch das Drücken des Trackpads im unteren Bereich wieder verlassen werden.

**Move State (Tower)**

Dieser State wird automatisch aktiviert, sobald der Spieler den Turm betritt. Er ist nun ebenfalls in der Lage, sich auf dem Turm zu bewegen, wie auch schon aus dem vorherigen Move State (Giant) bekannt. Hierbei ist die Fläche, auf der er sich bewegen kann, auf den Turm begrenzt. Deaktiviert wird dieser State, sobald der Spieler den Turm wieder verlässt.

**Brickboy State**

In diesem State ist der Spieler in der Lage, die roten Ziegelsteine aus der, auf dem Tower platzierten Kiste, durch das Gedrückthalten des Triggers aufzunehmen und wieder loszulassen. Wirft der Spieler einen dieser auf einen Bereich außerhalb des Turms, so entsteht eine Explosion, die Gegnern in einem Umkreis Schaden zufügt.

**Archer State**

Im Archer State bekommt der Spieler automatisch einen Bogen und einen Pfeil, der sich in den Bogen, mit Hilfe des Triggers, einspannen lässt. Wird der Trigger losgelassen, so wird der Pfeil abgeschossen. Trifft dieser auf dem Weg seiner Flugbahn einen Gegner, so fügt er diesem Schaden zu.

**Mage State**

Dieser State ermöglicht es dem Spieler, mächtige Zauber zu wirken. Hält der Spieler den Trigger eines Controllers gedrückt, so ist er in der Lage, eine der folgenden geometrischen Formen in der Luft und in seinem Sichtbereich zu zeichnen:

Kreis: Feuerzauber 
* 5 Schadenspunkte, großer Radius
Dreieck: Frostzauber 
* Gegner wird für 5 Sekunden um die Hälfte langsamer, mittlerer Radius
 Viereck: Verwirrungszauber 
* Gegner ist verwirrt und geht 2 Wegpunkte zurück, kleiner Radius


##Gesamteinschätzung
Das erste Problem war, wie so oft, der Anfang. Denn allein eine Idee für ein Projekt zu bekommen, ist nicht einfach, besonders da man an allen Enden merkt, dass dies und jenes eventuell nicht funktioniert, vielleicht zu langweilig oder viel zu umfangreich ist. Wir konnten uns recht schnell auf eine zu verwendende Technologie einigen: Virtual Reality. Aber wie? Im Rahmen der Veranstaltung hatten wir VR nur mit dem Google Cardboard kennengelernt, was ohne Frage vollkommen ausreicht, uns jedoch nicht spektakulär genug schien, um damit ein ganzes Projekt zu füllen. Letztlich kamen wir auf die Idee, dies mit einer HTC Vive umzusetzen.
Nachdem wir die erste Hürde, die Beschaffung dieser Technologie, durch unsere Beharrlichkeit überwunden hatten, begann unser sehr sportliches Rennen gegen die Zeit. Drei Wochen blieben uns nun für die Umsetzung unserer Projektidee. Diese ursprünglich simple Idee wuchs mit der Zeit und jeder Funktion die wir realisierten. Diese Zeitbegrenzung führte einerseits zu einem gewissen Grad an Stress, andererseits aber haben wir ihr einen schnellen Projektfortschritt zu verdanken. So folgte ein Erfolgserlebnis dem anderen und schweißte das Team zusammen.
Durch das doch recht rasante Voranschreiten des Projekts wurden wir optimistischer, auch den geplanten Teil für das Modul Computergrafik erfolgreich umsetzen zu können. Nachdem wir die erste HTC Vive wieder abgegeben hatten, war ein Großteil des gesamten Projektes schon so weit fertiggestellt, dass unser einziges Problem war, dass wir nun keine Brille mehr hatten, um weitere Fortschritte austesten zu können. Während der Zeit des Wartens begannen wir mit der visuellen Komplettüberholung in Blender.
Als wir dann die neue HTC Vive zur Verfügung gestellt bekommen haben, wurde das Projekt überarbeitet und mit vielen zusätzlichen und existenziellen Features versehen, die das Spiel letztlich erst spielbar und das Spielerlebnis für den Nutzer genießbar machten.
Während der gesamten Entwicklung stießen wir sowohl an mathematische, als auch hardwaretechnische Grenzen, die uns in Ideen, Algorithmen und der Umsetzung von spielerischen Elementen behinderten. So war es beispielsweise nicht möglich, unser Spiel mit Schatten oder einem dichteren Wald auszustatten, da dies für manche Hardware zu rechenintensiv gewesen wäre. Auch hatten wir Eingebungen, die aus dem Rahmen der von Unity gedachten Konzeption des Entity-Component-Systems fielen und uns zwischenzeitlich stark zurück warfen.

##Selbsteinschätzungen

>Während des Projekts war eine der größten Herausforderungen für mich, den Sinn hinter dem von Unity umgesetzten Konzept zu verstehen, der sich mir erst nach vielen Stunden erschlossen hat. Die Schnittstelle zwischen dem Code und den damit verbundenen „GameObjects“ und deren Zusammenwirken, habe ich erst nach einiger Zeit nachvollziehen können.
Dennoch habe ich selbst durch das Projekt gute Einblicke in Unity und dessen Funktionsweise bekommen. Auch konnte ich viele Aspekte der Programmierung von Spielen besser nachempfinden, während ich schon auf der anderen Seite erkannte, dass Unity doch auch unglaublich kompliziert werden konnte. Des Weiteren war es für mich während des Projektes sehr interessant zu sehen, dass gewisse mathematische Vorkenntnisse aus der Schule doch noch ihre Anwendung gefunden haben. Ob es um das Verstehen von Bewegungen im dreidimensionalen Raum oder um Schnittwinkelberechnung und -interpretation bei der Gestenerkennung ging. Auch wenn Quaternions wahrscheinlich noch eine längere Einarbeitungszeit benötigen würden, um diese wirklich zu verstehen und einsetzen zu können, so habe ich doch während des Projektes so manch mathematisches Prinzip wiedererkennen können.
Außerdem war es bemerkenswert zu sehen, wie sehr diese Technologie noch am Anfang der Entwicklung steht. Auch wenn die HTC Vive schon um einiges besser ist, als ein Google Cardboard, so hat man doch gemerkt, dass gerade die Bildschirmauflösung der Brille doch noch sehr gering ist. Ein Smartphone mit einer 4K Auflösung würde hierbei wohl deutlich besser abschneiden. Auf der anderen Seite ist es erstaunlich, wie präzise das Tracking der Brille und der Controller schon funktioniert und bin gespannt, was für Verbesserungen in den nächsten Jahren noch auf uns zukommen werden.

*~ Robin Wegner-Repke*

>Persönlich betrachtet war das Projekt für mich zeitlich sehr aufreibend und im Umfang für dieses Semester als sehr groß angesetzt. Dafür, dass ich zum ersten Mal mit Unity und auch mit einem Virtual Reality Head Mounted Display gearbeitet habe, lief die Einarbeitung und die anschließende Nutzung unproblematisch. Da ich bereits Vorkenntnisse in C# hatte, gelang mir das Arbeiten mit Unity auch bis auf weiteres nahtlos. Einzig das Entity-Component-System brachte mir und meinem Kommilitonen zyklisch neue Kopfschmerzen, da es viele bekannte und bewährte Entwurfsmuster aushebelt, sprich nicht nutzbar macht. Obwohl wir aufgrund der Knappen Zeit mitunter sehr gestresst an diesem Projekt gearbeitet haben muss ich sagen, dass ich sehr zufrieden mit dem Endresultat bin. Final kann ich sagen, dass ich voll hinter diesem Projekt stehe, da meinerseits viel Arbeit und Willenskraft eingeflossen ist und ich diese Anstrengungen ebenfalls von meinen Kommilitonen wahrgenommen habe. Ich hätte nicht gedacht, dass wir schlussendlich so ein sauberes Endprodukt entwickeln konnten. Gigantisch.

*~ Philipp Bönsch*

>Für mich persönlich stellte die Einarbeitung in das von Unity verwendete Entity-Component-System die größte Herausforderung dar, da sich dieses sehr stark vom Gedanken der Objektorientiertheit unterscheidet. Ebenso hat der Unity Editor mit seinem stellenweise sehr gewöhnungsbedürftigen Verhalten mich das eine oder andere mal die Stirn runzeln lassen.
In meinen Augen haben wir den Arbeitsaufwand gut zwischen uns drei aufteilen können. Hierzu hat die Tatsache, dass wir immer nur als Gruppe an dem Projekt arbeiten konnten stark beigetragen. Somit konnte jeder seine stärken einbringen und hatte die Möglichkeit sich in allen Teilbereichen zu verwirklichen. 
Durch dieses Projekt habe ich einen sehr schönen Einblick in die Entwicklung von Computerspiele wie auch den Umgang von VR-Headsets erlebt. Darüberhinaus konnte man viele Erfahrungen im Umgang mit 3D-Umgebungen sammeln. 
Zu guter Letzt sollte gesagt sein, dass es trotz des straffen Zeitplans ein sehr spaßiges und lehrreiches Projekt war und ich es jederzeit wieder durchführen würde.

*~ Ralph Schlett*

##Literaturverzeichnis

[^td]:Das **Tower Defense** Genre ist ein Subgenre des [Echtzeit-Strategiespiels](https://www.techopedia.com/definition/1923/real-time-strategy-rts). Die Hauptaufgaben des Spieles bestehen darin verschiedene Arten von Türmen (Verteidigungsanlagen) aufzustellen. Diese sollen Wellen von Gegnern vor dem durchqueren der Karte und dem anschließendem zerstören der Spielerbasis (Gebäude am Ende des Pfades) hindern.

[^enemywave]: Eine **Gegner Welle** ist Ansammlung von Gegnern, wobei diese in gleichmäßigen Zeitabständen aufeinanderfolgend beginnen dem Pfad  zu betreten.
