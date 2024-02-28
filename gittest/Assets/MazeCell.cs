using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
   [SerializeField] private GameObject _leftwall;
   [SerializeField] private GameObject _rightwall;
   [SerializeField] private GameObject _frontwall;
   [SerializeField] private GameObject _backwall;
   [SerializeField] private GameObject _unvisitedBlock;

   public bool Isvisited { get; private set; }

   public void Visit()
   {
       Isvisited = true;
       _unvisitedBlock.SetActive(false);
   }
   public void Clearleft()
   {
       _leftwall.SetActive(false);
   }
   public void Clearright()
   {
       _rightwall.SetActive(false);
   }
   public void Clearfront()
   {
       _frontwall.SetActive(false);
   }
   public void Clearbackt()
   {
       _backwall.SetActive(false);
   }







}
