# Projet "La petite Princesse"

Bienvenue dans notre jeu vidéo créé par Julien ORIOL, Louis DEDIEU et Tristan LARGUIER.

## Lien de téléchargement

https://

## Scénario

Le scénario de notre jeu est le suivant :
> Il était une fois une petite princesse qui s'ennuyait beaucoup sur sa planète. Curieuse et aventurière, cette-dernière entendit parler de la planète Terre, et eu envie de venir la visiter. 
Elle se diriga alors vers celle-ci, et commença par visiter ce que les humains avaient créé de leurs propres mains via la planète Musée. Cependant, après avoir visité les différentes oeuvres, la chute d'un arbre secoua la planète Terre, et la petite princesse prit peur et voulut s'échapper de là pour rentrer chez elle. Un portail apparu alors face à elle, et la téléporta immédiatement dans la planète course. Elle comprit alors qu'elle devrait remporter cette course si elle voulait avoir une chance de pouvoir rentrer chez elle. Une fois remporté, un nouveau portail apparu et la transporta sur la dernière planète, la planète du parcours de l'équilibriste. Un robot lui expliqua alors qu'elle devrait compléter trois parcours de l'équilibriste pour pouvoir enfin rentrer chez elle… Suite à une démonstration hors pair de son équilibre et de sa patience, le robot la félicita et la petite princesse pu enfin rentrez chez elle.

## Assets, ressources et tutoriaux utilisés

Pour faire fonctionner ce projet, aucune libraire externe n'est nécessaire.

### Ressources communes aux 3 planètes

Sur les trois différentes planètes, nous avons importé sans modification les éléments suivants :
- Une bande son dans le thème "Espace" trouvée sur YouTube : https://youtu.be/jRPy70dAcmU
- Le personnage Unity Chan qui joue le rôle de notre princesse tout le long du jeu : https://assetstore.unity.com/packages/3d/characters/unity-chan-model-18705
- Divers portails pour passer d'une planète à une autre lors d'évènements clé : https://assetstore.unity.com/packages/3d/environments/fantasy/the-portal-collection-205438
- Une image de fond spatiale prise sur Internet pour mettre un décor

Aucun tuto particulier n'a été suivi pour installer ces assets.

En ce qui concerne les scripts servant à implémenter les différentes logiques de notre jeu, ces-derniers ont tous été rédigés à la main. Seul les scripts d'animations et de déplacements de notre princesse ont été importés, quoique légèrement modifiés, notamment pour enlever certains affichages relatif au personnage initialement présents sur l'écran.

### Planète de la course

Sur cette planète, les seuls imports utilisés sont le modèle du circuit de voiture utilisé en TD et des bruits de voitures.

Néanmoins, de nombreuses améliorations ont pu être apporté par rapport au TD initial sur la création d'une course de voiture :
- La modification de la couleur du circuit pour mieux convenir au thème du jeu
- La mise en place d'une arche et d'une grille de départ
- La création à la main et l'utilisation de voitures dans le style Formule 1
- La mise en place d'une animation de boost lorsqu'une certaine vitesse est atteinte par notre voiture
- La mise en place de sons de voiture
- Le développement d'un système d'IA innovant se basant sur des points à suivre en fonction de leur position dans le circuit
- La visite du circuit avant le départ de la course
- L'ajout de divers messages tout au long du circuit (nombre de tours, compteur avant le départ, message de fin, …)
- La mise en place d'un classement
- La possibilité de rejouer la course dans le cas où l'on ne fini pas 1er

A nouveau, aucun tutoriel particulier n'a été suivi pour mettre en place tout cela.

### Planète du musée

Sur cette planète, la majorité du contenu a été réalisé via des assets qui ont ensuite été modifié pour les besoins de la planète. A l'exception des modèles blenders de chaque membre du groupe.
Les diffèrents assets importés sont :
- Le musée, qui a été aménagé en supprimant/modifiant des pièces, notamment pour incorporer le terrain de basket, et la rampe pour accéder a l'arbre.
- La rampe (https://www.cgtrader.com/items/190589/download-page) permettant d'accéder à l'arbre via le tunnel. 
- Le tunnel qui suit la rampe (https://www.turbosquid.com/3d-models/square-tunnel-rotation-low-poly-1762165) et qui mène a l'arbre a été entouré de matériaux noirs pour augmenter le contraste.
- Le dome qui contient l'arbre (https://sketchfab.com/3d-models/sci-fi-future-building-2-simple-dome-d885fdb25ed846779fc711917fbeeed2).
- Le sol du dome est un terrain aménagé notamment grâce aux layers de ce pack (https://assetstore.unity.com/packages/3d/vegetation/environment-pack-free-forest-sample-168396)
- Le portail vient du pack de portails mentionné plus tôt.

En ce qui concerne les tutoriaux suivis, nous nous sommes aidé des suivants :
- Le rétroprojecteur a été réalisé grâce à cette vidéo youtube (https://www.youtube.com/watch?v=uU26wucC02s&t=54s) il suffisait ensuite d'importer la vidéo souhaitée et d'ajuster les différents paramètre pour notre cas.
- Déclencher des animations lors d'un évènement (https://www.youtube.com/watch?v=npCqMT-M2EQ&t=427s)
- Afficher un texte lors d'un contacte avec un collider (https://www.youtube.com/watch?v=NTkqHGi-sPA)
- Comment faire apparaitre des objets (https://www.youtube.com/watch?v=_Xrw2EEhzI4&t=324s)



### Planète du parcours de l'équilibriste

Pour cette planète, les modèles qui ont été créés entièrement à la main sont :
- Les différents parcours, correspondant aux scènes « Level1 », « Level2 » et « Level3 »
- Le chemin de la scène « SpaceScene »

En ce qui concerne les import utilisés pour cette planète, on peut retrouver :
- Des portails de la même famille que ceux utilisés pour se déplacer entre les planètes, notamment pour se téléporter de la scène "SpaceScene" aux différents niveaux, et à la fin de chaque niveau pour revenir à la scène "SpaceScene"
- Les barrières utilisées pour délimiter le chemin dans l’espace : https://assetstore.unity.com/packages/3d/props/exterior/stone-fence-2437
- La plateforme en pierre avec une tête de mort au centre servant de plateforme pour héberger les portails menant aux différents niveaux : https://assetstore.unity.com/packages/3d/props/skull-platform-105664
- Les différents objets célestes (Terre, Lune, astéroïdes) utilisées en fond de la « SpaceScene » et des différents niveaux : https://assetstore.unity.com/packages/3d/environments/planets-of-the-solar-system-3d-90219
- Le robot avec qui on peut dialoguer sur la « SpaceScene » : https://assetstore.unity.com/packages/3d/characters/jammo-character-mix-and-jam-158456
- La boîte de dialogue avec le robot : https://assetstore.unity.com/packages/tools/utilities/dialogue-editor-168329

En ce qui concerne les tutoriaux suivis, nous nous sommes aidé des suivants :
- Pour permettre des transitions fluides entre les différentes scènes : https://youtu.be/_9L0HJrVR5Y
- Pour mettre en place une boîte de dialogue avec le bot : https://youtu.be/QPJHY6MPag4

## Conclusion

Merci d'avoir joué à notre jeu, nous espérons qu'il vous a plu et que vous êtes parvenus à le terminer sans trop de difficulté :)