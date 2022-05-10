using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrypts.Entity
{
    public class PathManager
    {
        public static PathManager pathManager = new PathManager();
        private List<Vector2> purposes;
        private Dictionary<Vector2, Vector2> snippets;

        private PathManager()
        {
            snippets = new Dictionary<Vector2, Vector2>();
        }
        //уничтожаем связи для уничтоженного владения
        public void DestroyFortress(Vector2 deadFortressPosition)
        {
            purposes.Remove(deadFortressPosition);
            Vector2[] keys = new Vector2[snippets.Count];
            snippets.Keys.CopyTo(keys, 0);

            snippets = new Dictionary<Vector2, Vector2>();
            foreach (Vector2 currentshelter in keys)
                snippets.Add(currentshelter, AlgoritmicPoint(currentshelter, keys));
        }
        //получение точек владений
        public Vector2[] GetFortress() => purposes.ToArray();
        //ближайшее владение
        public Vector2 ClosestFortress(Vector2 position)
        {
            if (purposes.Count == 0)
                return Vector2.zero;
            float length = (purposes[0] - position).magnitude;
            Tuple<Vector2, float> preferTarget = new Tuple<Vector2, float>(purposes[0], length);
            for (int i = 1; i < purposes.Count; i++)
            {
                length = (purposes[i] - position).magnitude;
                if (length < preferTarget.Item2)
                    preferTarget = new Tuple<Vector2, float>(purposes[i], length);
            }
            return preferTarget.Item1;
        }
        //ищем путь
        public Vector2[] SearchPath(Vector2 EntityPosition)
        {
            Vector2[] points = new Vector2[snippets.Count];
            snippets.Keys.CopyTo(points, 0);
            Vector2 preferendPoint = AlgoritmicPoint(EntityPosition, points);

            List<Vector2> path = new List<Vector2>();
            Debug.Log(preferendPoint);
            while (snippets.ContainsKey(preferendPoint))
            {
                path.Add(preferendPoint);
                preferendPoint = snippets[preferendPoint];
            }
            path.Add(preferendPoint);
            return path.ToArray();
        }
        //создаем список связей
        public void CreateNodeList(Vector2[] shelters)
        {
            GameObject[] fortresses = GameObject.FindGameObjectsWithTag("Fortress");
            purposes = new List<Vector2>();
            for (int i = 0; i < fortresses.Length; i++)
                purposes.Add(fortresses[i].transform.position);

            List<Vector2> sheltersTmp = new List<Vector2>(shelters);
            for(int i = 0; i < sheltersTmp.Count; i++)
                for(int j = i; j < sheltersTmp.Count; j++)
                {
                    if (shelters[i].y < shelters[j].y)
                    {
                        Vector2 tmp = shelters[i];
                        shelters[i] = shelters[j];
                        shelters[j] = tmp;
                    }
                } 
                    

            snippets.Clear();
            foreach (Vector2 currentShelter in shelters)
            {
                if (snippets.ContainsKey(currentShelter))
                    continue;

                bool isIn = false;
                foreach (Vector2 purpose in purposes)
                    if (currentShelter == purpose)
                    {
                        isIn = true;
                        break;
                    }
                if (isIn)
                    continue;

                snippets.Add(currentShelter, AlgoritmicPoint(currentShelter, shelters));
            }
        }
        //очистить данные
        public void Clear()
        {
            purposes = null;
            snippets.Clear();
        }
        //ищем подходящую по алгоритму точку для данной
        private Vector2 AlgoritmicPoint(Vector2 position, Vector2[] points)
        {
            float length, angle;//, distance;
            Tuple<Vector2, float, float> preferendPoint = new Tuple<Vector2, float, float>(Vector2.negativeInfinity, float.MaxValue, 180);
            foreach (Vector2 shelter in points)
                foreach (Vector2 purpose in purposes)
                {
                    if (snippets.ContainsKey(shelter))
                        if(snippets[shelter] == position)
                            continue;

                    length = (position - shelter).magnitude;
                    angle = Vector2.Angle(purpose - shelter, position - shelter);
                    if (length < preferendPoint.Item2 && angle > 90)
                        preferendPoint = new Tuple<Vector2, float, float>(shelter, length, angle);
                }
            if (preferendPoint.Item3 == 180)
            {
                //Debug.Log(ClosestFortress(position));
                return ClosestFortress(position);
            }
            //Debug.Log($"{position} | {preferendPoint.Item1} | {preferendPoint.Item3}");
            return preferendPoint.Item1;
        }
    }
}
