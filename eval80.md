# Évaluation intermédiaire (80 %) – Bixente Pouradier

## Synthèse

Le prototype est fonctionnel mais très incomplet. Le joueur se déplace (avec passage d'un bord à l'autre) et tire automatiquement quand il est immobile. **Un seul ennemi** fait des allers-retours, descend et tire quand le joueur passe sous lui. Il n'y a ni obstacles, ni score, ni fin de partie, et l'ennemi ne meurt jamais. Le rapport n'a pas été complété depuis septembre.

## Points à améliorer d'ici au rendu final (30.10)

1. Créer **≥ 10 ennemis** (boucle), **5 obstacles** (classe `Obstacle`), un **score**, une **victoire** et une **défaite**.
2. Supprimer les ennemis morts (parcours à l'envers + `RemoveAt`).
3. Encapsuler (champs privés + propriétés) et remplacer `type` par un `enum` ou deux classes.
4. Compléter le rapport (réalisation, tests, conception, IA, bilans).
