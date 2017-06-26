
#VR Giant
[TOC]
##Vorwort
Das hier beschriebene Spiel entstand im Rahmen eines Projektes in den Hochschulmodulen "Entwicklung von Multimediasystemen" und "Computergrafik" an der Hochschule für Technik und Wirtschaft Berlin. Die daran beteiligten Studenten waren *R.Schlett*, *R.Wegner-Repke* und *P.Bönsch*.

##Abstract
VR Giants ist ein Virtual Reality Spiel, welches exklusiv für die HTC Vive entwickelt wurde. Das Spiel fällt in das Genre **Tower Defense**[^td]. Mittels [Vive Controller](https://www.vive.com/us/accessory/controller/) kann sich der Spieler in der Welt bewegen, neue Türme in der Spielwelt platzieren und bereits platzierte Türme betreten. Auf Türmen können offensive Fähigkeiten gewirkt werden, um die Wellen von Gegnern vor dem erreichen der eigenen Basis zu hindern.

##Spiel
###Beschreibung
####Spielelemente
#####Welt
Die Welt ist ein von Bergen umrandetes Tal, durch welches sich ein geschwungener Pfad zieht. Am Ende des Pfades befindet sich die Basis des Spielers.

___
#####Türme
Der Spieler hat die Möglichkeit entlang des Pfades 3 verschiedene Arten von Türmen - *Archer Tower*, *Brickboy Tower* und *Mage Tower* - zu platzieren. Alle Türme verfügen über einen Auto-Attack der ein Projektil in Flugrichtung der Gegner startet. Bei auftreffen des Projektils werden dem Gegner Lebenspunkte abgezogen.
Abhängig von der Turmart ermöglicht es der Turm in 3 verschiedene Spielerrollen - *Archer*, *Brickboy*, *Mage* zu wechseln. Dies geschieht beim betreten eines Turms.

 - Die **Archer** Rolle bietet dem Spieler die Funktionalität mit einem Bogen und zyklisch neu erstellten Pfeilen dem Gegner Lebenspunkte abzuziehen.
 - Bei der **Brickboy** Rolle existiert auf dem Turm eine *Brick Chest*, welche zyklisch neue *Exploding Bricks* erstellt. Diese können vom Spieler aufgehoben und auf die Spielwelt geworfen werden. Dabei entsteht eine Explosion, die Gegnern im Explosionsradius Lebenspunkte abzieht.  
 - In der **Mage** Rolle besitzt der Spieler die Fähigkeiten durch das Durchführen von Gesten 3 verschiedene Zauber zu wirken (*Eiszauber, Feuerzauber, Blitzzauber*).

___
#####Basis
Die *Basis* ist das Hauptgebäude des Spielers, welche es zu verteidigen gilt. Erreichen Gegner die Basis lösen diese sich in einer Rauchwolke auf und ziehen der Basis Lebenspunkte ab. Fallen die Lebenspunkte der Basis auf 0, geht diese in einem Feuerwerk auf. Der Spieler hat dann das Spiel verloren.

___
#####Gegner
Der Spieler muss sich im Verlauf des Spiels gegen Wellen[^enemywave] von Gegnern behaupten. Ziel der Gegner ist es dem Pfad entlang bis zur Basis zu folgen um dieser Lebenspunkte abzuziehen. Die Gegner sind unterteilt in 3 verschiedene Gegnertypen: *Normal Enemy*, *Fast Enemy* und *Resistance Enemy*.

 - Der *Resistance Enemy* hat eine prozentuale Resistenz gegen jegliche Art von Angriffen, was ihm eine längere Existenz ermöglicht.
 - Der *Fast Enemy* hat eine um Faktor x erhöhte Bewegungsgeschwindigkeit.
 - Der *Normal Enemy* besitzt weder eine erhöhte Bewegungsgeschwindigkeit, noch eine Resistenz gegen erlittene Schäden.

Fallen die Lebenspunkte eines Gegners durch das Eintreffen eines Projektils oder durch Angriffsaktionen des Spielers auf 0, lösen sich die Gegner in einer Rauchwolke auf.

___
####Ziel
Ziel des Spiels ist es möglichst alle Gegner von dem Ankommen in der Basis abzuhalten, sprich alle Gegnerwellen zu überstehen, ohne dabei die Lebenspunkte der *Basis* auf 0 sinken zu lassen.

###Steuerung

###Implementierung

[^td]:Das **Tower Defense** Genre ist ein Subgenre des [Echtzeit-Strategiespiels](https://www.techopedia.com/definition/1923/real-time-strategy-rts). Die Hauptaufgaben des Spieles bestehen darin verschiedene Arten von Türmen (Verteidigungsanlagen) aufzustellen. Diese sollen Wellen von Gegnern vor dem durchqueren der Karte und dem anschließendem zerstören der Spielerbasis (Gebäude am Ende des Pfades) hindern.

[^enemywave]: Eine **Gegner Welle** ist Ansammlung von Gegnern, wobei diese in gleichmäßigen Zeitabständen aufeinanderfolgend beginnen dem Pfad  zu betreten.

----
[1]: http://math.stackexchange.com/
[2]: http://daringfireball.net/projects/markdown/syntax "Markdown"
[3]: https://github.com/jmcmanus/pagedown-extra "Pagedown Extra"
[4]: http://meta.math.stackexchange.com/questions/5020/mathjax-basic-tutorial-and-quick-reference
[5]: https://code.google.com/p/google-code-prettify/
[6]: http://highlightjs.org/
[7]: http://bramp.github.io/js-sequence-diagrams/
[8]: http://adrai.github.io/flowchart.js/
