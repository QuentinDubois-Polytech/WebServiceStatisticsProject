# Projet WebService

**Auteur : Quentin Dubois**

**Date de rendu : 14/04/2023**

## Données de sites utilisées
Les statistiques sont calculées sur une base de 13 sites parmis les plus visités au monde. Les sites sont stockés dans le fichier ressources.txt dans le projet StatisticsProjet à la racine du projet. Il est donc totalement possible de modifier les sites utilisés pour le calcul des statistiques. J'en ai choisi 13 car c'est un nombre suffisament faible pour éviter d'attendre longtemps le résultats des statistiques.

## Justification des choix sur le calcul des statistiques

J'ai choisi de travailler sur les headers Last-Modified, Alt-Svc et Cookie.

### Last-Modified
Le header Last-Modified contient la date et l'heure à laquelle le serveur d'origine pense que la ressource a été modifiée pour la dernière fois.

J'ai chosi ce champ car il permet de voir la fréquence de modification des ressources des sites web. Nous en déduisons que le temps moyen de modifications de la page web principal des sites web les plus visités est d'environ 5H. Ceci est relativement faible. 

Il est tout à fait possible d'utiliser se header afin de vérifier que les ressources d'un site web ont bien toutes été mise à jour et déceler de potentiels problèmes de mise à jour.

### Alt-Svc
L'en-tête HTTP Alt-Svc permet à un serveur d'indiquer qu'un autre emplacement du réseau (le "service alternatif") peut être considéré comme faisant autorité pour cette origine lors de futures requêtes.

J'ai choisi ce champ car il permet d'indiquer à l'utilisateur que le même site web est également disponible sur un autre serveur. Cela peut notamment être utile si le serveur principal est en maintenance ou si le serveur principal est surchargé. Nous remarquons cependant que ce champ est utilisé par environ 31% sur la base utilisée.
Ce champ est donc peu utilisé, par rapport à ce que l'on pourrait s'attendre.

### Cookie
J'ai choisi de calculer les nombre de cookie moyen envoyé à la connexion de la page web principale. On remarque que le nombre de cookie moyen est d'environ 1,5. Cela est relativement faible. On aurai eu tendance à penser que la quantité de cookie acquise aurait été plus importante.

Le champ cookie est bien évidemment une quantité d'informations intéressante à connaitre. Il permet de savoir si un site web utilise des cookies et de savoir combien de cookies sont envoyés à la connexion de la page web principale. Il permet également de savoir si le site web utilise des cookies pour stocker des informations sur l'utilisateur.

## Fonctionnement du programme

### Explication de la structure du projet

J'ai tout d'abord 3 projets qui s'occupent d'envoyer des requêtes HTTP et de calculer les statistiques sur certains headers. Tous ces projet ont une méthode qui permet de spécifier les urls à parcourir et de récupérer le résultat des statistiques encapsulé dans un objet.

J'ai un 4ème projet, StatisticsProject qui s'occupe de réaliser le rôle de serveur HTTP. Il répond à des requêtes API sur la sous-route /api et renvoie les objets en JSON correspondant. Il permet également de récupérer les ressources statiques (HTML, CSS, JS) qui sont stockées dans le dossier www/pub/.

### Comment lancer le programme

Pour lancer le programme, il faut lancer le projet StatisticsProject qui est le serveur HTTP. Il suffit ensuite de lancer un navigateur web et d'aller sur l'adresse http://localhost:8080/index.html. A l'aide de la page web, il est possible de lancer le calculs du statistiques des 3 projets distincts. Les résultats sont affichés dans la page web.

Attention ! Les requêtes HTTP prennent du temps à s'exécuter. Il faut donc attendre dans le pire des cas 1 minute pour avoir les résultats. Il est également fortement conseiller de lancer le calcul des statistiques les uns après les autres.

Dans l'éventualité où le programme prend trop de temps, voici une image qui montre le résultat des statistiques déjà calculées :
[image](./img/statisticsComputed.png)

