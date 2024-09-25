using UnityEngine;
using ServiceLocator.Wave.Bloon;
using ServiceLocator.Main;
using System.Collections.Generic;

namespace ServiceLocator.Player.Projectile
{
    public class ProjectileController
    {
        private ProjectileView projectileView;
        private ProjectileScriptableObject projectileScriptableObject;

        private BloonController target;
        private ProjectileState currentState;
        private PlayerService playerService;

        private int projectileHitCont = 0;

        public ProjectileController(ProjectileView projectilePrefab, Transform projectileContainer, PlayerService playerService)
        {
            projectileView = Object.Instantiate(projectilePrefab, projectileContainer);
            projectileView.SetController(this);
            this.playerService = playerService;
            projectileHitCont = 0;
        }

        public void Init(ProjectileScriptableObject projectileScriptableObject)
        {
            this.projectileScriptableObject = projectileScriptableObject;
            projectileView.SetSprite(projectileScriptableObject.Sprite);
            projectileView.gameObject.SetActive(true);
            target = null;
        }

        public void SetPosition(Vector3 spawnPosition) => projectileView.transform.position = spawnPosition;

        public void SetTarget(BloonController target)
        {
            this.target = target;
            SetState(ProjectileState.ACTIVE);
            RotateTowardsTarget();
        }

        private void RotateTowardsTarget()
        {
            Vector3 direction = target.Position - projectileView.transform.position;
            float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 180;
            projectileView.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        public void UpdateProjectileMotion()
        {
            if(target != null && currentState == ProjectileState.ACTIVE)
                projectileView.transform.Translate(Vector2.left * projectileScriptableObject.Speed * Time.deltaTime, Space.Self);
        }

        public void OnHitBloon(BloonController bloonHit)
        {
            if (currentState == ProjectileState.ACTIVE)
            {
                projectileHitCont++;
                bloonHit.TakeDamage(projectileScriptableObject.Damage);

                if(projectileHitCont >= projectileScriptableObject.Piercing)
                {
                    if(projectileScriptableObject.BlastRadius > 0f)
                    {
                        CheckBlastRadious();
                    }

                    ResetProjectile();
                    SetState(ProjectileState.HIT_TARGET);
                }
            }
        }

        private void CheckBlastRadious()
        {
            var contactFilter = new ContactFilter2D();
            List<Collider2D> results = new List<Collider2D>();

            int numberOfHits = Physics2D.OverlapCircle(projectileView.transform.position, projectileScriptableObject.BlastRadius, contactFilter.NoFilter(), results);

            for (int i = 0; i < numberOfHits; i++)
            {
                Collider2D hitCollider = results[i];
                
                if(hitCollider.gameObject.TryGetComponent<BloonView>(out BloonView bloonView))
                {
                    bloonView.Controller.TakeDamage(projectileScriptableObject.Damage);
                }
            }
        }

        public void ResetProjectile()
        {
            target = null;
            projectileView.gameObject.SetActive(false);
            playerService.ReturnProjectileToPool(this);
            projectileHitCont = 0;
        }

        private void SetState(ProjectileState newState) => currentState = newState;

        private enum ProjectileState
        {
            ACTIVE,
            HIT_TARGET
        }
    }
}