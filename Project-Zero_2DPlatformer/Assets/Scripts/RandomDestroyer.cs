using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDestroyer : MonoBehaviour
{
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    private int randomDestroyer;
    private int destroyByOverWalking = 1;
    

=======
    int randomDestroyer;
>>>>>>> parent of 5b821bd... ruumiin päälle tallominen työnalla
    public int maxRandomRangeRifle = 0;
    public int maxRandomRangeShotGun = 0;
    public int maxRandomRangeMissile = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (Player_Weapons.weapon == 3) // missile
=======
    int randomDestroyer;
    // Start is called before the first frame update
    void Start()
    {
        if (Player_Weapons.weapon == 3)
>>>>>>> parent of 9df1d5f... zombeilla on vipuvarsi
=======
    int randomDestroyer;
    // Start is called before the first frame update
    void Start()
    {
        if (Player_Weapons.weapon == 3)
>>>>>>> parent of 9df1d5f... zombeilla on vipuvarsi
        {
            randomDestroyer = Random.Range(1, 3);
        } else {
            randomDestroyer = Random.Range(1, 10);
        }

        if (randomDestroyer == 1) {
            Destroy(gameObject, 0.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
<<<<<<< HEAD
<<<<<<< HEAD

    private void Destroy()
    {
        Destroy(gameObject, 0.05f);
    }
<<<<<<< HEAD

    void OnTriggerEnter2D()
    {
        destroyByOverWalking++;
        if (destroyByOverWalking < 0)
        {
            Debug.Log("ruumis tuhoutuu");
            Destroy(gameObject);
        }

    }
=======
>>>>>>> parent of 9df1d5f... zombeilla on vipuvarsi
=======
>>>>>>> parent of 9df1d5f... zombeilla on vipuvarsi
=======
>>>>>>> parent of 5b821bd... ruumiin päälle tallominen työnalla
}
