using System.Collections;
using UnityEngine;

namespace Script
{
    public class CreditHandler : MonoBehaviour
    {
        public GameObject credit;
        
        private IEnumerator CreditStart()
        {
            credit.transform.position += new Vector3(0, 1, 0);
            yield return new WaitForSeconds(30f);
        }

        public void ButtonCreditHandler()
        {
            StartCoroutine(CreditStart());
            
        }
    }
}
