using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public interface Idestractebl
{
    float Health { get; set; }
    void RecieveHit(float damage);
    void Die();
 

}
