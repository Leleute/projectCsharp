# C# Project - WPF Application MVVM pattern

Ce projet est une application WPF en C#, utilisant l'architecture MVVM.<br>
Nous utilisons les packages NuGet XamlAnimatedGif et Syncfusion.Sfchart.WPF dans cette application.<br>

## Pour lancer l'application :
Clonez ce repository git<br>
Allez dans le dossier projectCsharp<br>
Lancez la solution projetCsharp via Visual Studio<br>

## Concept du projet :
Nous avons decidé de faire une application permettant de suivre l'evolution du Coronavirus dans le monde.<br>
Pour ce faire nous utilisons l'api https://covid19.mathdro.id/api/confirmed. <br>
Cette API nous permet de recenser par pays ou region :<br>
  - Le nombre de cas confirmés au total<br>
  - Le nombre de personnes mortes<br>
  - Le nombre de personnes qui ont guéri du virus<br>
  - Le nombre de personnes ayant contracté le virus<br>

## Mode d'emploi :

Une fois l'application lancée, vous pouvez observer trois differentes parties.<br>
- Une partie `Information` expliquant le but de l'application.
- Une partie `Options d'affichage` permettant de choisir entre un affichage par tableau ou par graphique.
- Une partie `Paramètre d'affichage`vous permettant de choisir les informations que vous voulez voir,<br> 
  Selon quels critères les trier et le nombre d'information que vous voulez afficher.<br>
  Vous pouvez choisir d'afficher une ou plusieurs des informations entre les cas confirmés, les personnes mortes, les personnes qui ont guéri et les personnes ayants le virus<br>
  Vous pouvez choisir de trier par region ou par pays. <br>
  *A noter que pour beaucoup de pays, les statistiques par régions ne sont pas disponible. Quand cela est le cas, le nom de la region sera celui du pays, les statisques par regions ne sont présentes que pour les pays les plus grands et touchés (Etats Unis, Chine)*<br>
  Vous avez la possibilité de faire une recherche par nom, vous pouvez utiliser la barre "Recherche Spécifique" pour rentrer une chaine de caractére qui devra etre contenu dans le nom du pays ou de la région de la recherhe.
  Le nombre de données étant très élevées, vous avez la possibilité pour éviter des lags ou des freezes, de limiter le nombre de sortie de l'algorithme.<br>
  Une fois vos choix faits, vous pouvez utiliser le bouton "Chercher" pour que les resultats s'affichent.<br>

## Membres de l'équipe :

François--Bachelier Clément<br>
Le Guern Bastien<br>
Leleu Matthieu<br>

### Merci beaucoup



