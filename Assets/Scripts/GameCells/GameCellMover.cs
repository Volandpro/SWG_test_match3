using System.Collections;
using Field;
using Infrastructure.Misc;
using Infrastructure.States.GameLoopStates;
using UnityEngine;
using Zenject;

namespace GameCells
{
    [RequireComponent(typeof(GameCell))]
    public class GameCellMover : MonoBehaviour
    {
        private GameLoopStateMachine stateMachine;
        public GameCell gameCellRoot;

        [Inject]
        public void Construct(GameLoopStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        private void Awake()
        {
            gameCellRoot = this.GetComponent<GameCell>();
            gameCellRoot.OnMove += MoveTo;
        }

        private void OnDisable()
        {
            gameCellRoot.OnMove -= MoveTo;
        }

        private void MoveTo(FieldCell fieldCell)
        {
            StopAllCoroutines();
            StartCoroutine(MoveToCour(fieldCell.transform.position));
        }

        private IEnumerator MoveToCour(Vector3 targetPosition)
        {
            Vector3 startPosition = transform.position;
            float distance = Vector3.Distance(startPosition, targetPosition);
            float currentTimer = 0;
            while (transform.position!=targetPosition)
            {
                this.transform.position = Vector3.Lerp(startPosition, targetPosition, currentTimer);
                currentTimer += Time.deltaTime * GlobalCachedParameters.CellsMoveSpeed/distance;
                yield return null;
            }
            FinishedMove();
        }
        private void FinishedMove()
        {
            gameCellRoot.isMoving = false;
            stateMachine.ActiveStateFinishedMove(gameCellRoot);
        }
    }
}