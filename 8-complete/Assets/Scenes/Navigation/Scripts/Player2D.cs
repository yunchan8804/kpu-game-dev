using UnityEngine;

namespace Scenes.Navigation
{
    using System;
    using Pathfinding;

    public class Player2D : MonoBehaviour
    {
        private Seeker _seeker;
        private AIPath _aiPath;
        private Rigidbody2D _rigidbody2D;
        private Path _path;
        private Camera _camera;
        private int _currentPoint;
        [SerializeField] private float speed = 10;


        private void Awake()
        {
            _seeker = GetComponent<Seeker>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _camera = FindObjectOfType<Camera>();
        }

        private void Start()
        {
            Seek();
        }

        private void Update()
        {
            if (_path == null)
                return;

            var targetPosition = new Vector3(_path.vectorPath[1].x, _path.vectorPath[1].y, 0);
            print(targetPosition);
            _rigidbody2D.velocity = (targetPosition - transform.position).normalized * (Time.deltaTime * speed);
        }

        private void Seek()
        {
            var targetPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            print(targetPosition);
            _seeker.StartPath(transform.position, targetPosition, OnPath);
        }

        private void OnPath(Path p)
        {
            if (p.error)
                return;

            _path = p;
            _currentPoint = 0;
            Seek();
        }
    }
}