using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Units : MonoBehaviour
{
    private string unitName;
    private string charName;
    private float attDmg;
    private float attSpeed;
    private float def;
    private float health;
    private float moveSpeed;
    private float rotate;
    private float range;
    private bool isAlive;
    private bool isSelect;
    private int unitCost;
    private int unitWorth;

    private float heightDiff;

    private float angNormal;

    private Units firstContact;
    private Units enemy;
    private GameObject directEnemy;

    private Vector3 moveLoc;
    private List<Vector3> positions;

    private bool canDo;

    // Description?
    // Rank?
    // House?
    // Abilities?
    // Unit Count?

    public string GetUnitName() { return unitName;  }
    public string GetCharName() { return charName; }
    public float GetAttDmg() { return attDmg; }
    public float GetAttSpeed() { return attSpeed; }
    public float GetDef() { return def; }
    public float GetHealth() { return health; }
    public float GetMoveSpeed() { return moveSpeed; }
    public float GetRotate() { return rotate; }
    public float GetRange() { return range; }
    public bool GetIsAlive() { return isAlive; }
    public bool GetIsSelect() { return isSelect; }
    public int GetUnitCost() { return unitCost; }
    public int GetUnitWorth() { return unitWorth; }

    public Units GetFirstContact() { return firstContact; }
    public Units GetEnemy() { return enemy; }
    public GameObject GetDirectEnemy() { return directEnemy; }

    public List<Vector3> GetPosList() { return positions; }

    public void SetUnitName(string _unitName) { unitName = _unitName; }
    public void SetCharName(string _charName) { charName = _charName; }
    public void SetAttDmg(float _attDmg) { attDmg = _attDmg; }
    public void SetAttSpeed(float _attSpeed) { attSpeed = _attSpeed; }
    public void SetDef(float _def) { def = _def; }
    public void SetHealth(float _health) { health = _health; }
    public void SetMoveSpeed(float _moveSpeed) { moveSpeed = _moveSpeed; }
    public void SetRotate(float _rotate) { rotate = _rotate; }
    public void SetRange(float _range) { range = _range; }
    public void SetIsAlive(bool _isAlive) { isAlive = _isAlive; }
    public void SetIsSelect(bool _isSelect) { isSelect = _isSelect; }
    public void SetUnitCost(int _unitCost) { unitCost = _unitCost; }
    public void SetUnitWorth(int _unitWorth) { unitWorth = _unitWorth; }

    public void SetFirstContact(Units _firstContact) { firstContact = _firstContact; }
    public void SetEnemy(Units _enemy) { enemy = _enemy; }
    public void SetDirectEnemy(GameObject _directEnemy) { directEnemy = _directEnemy; }

    void Start()
    {
        positions = new List<Vector3>();
        canDo = true;
    }

    void Update()
    {
        
    }

    public void AddPos(Vector3 pos)
    {
        positions.Add(pos);       
    }

    public void moveTo()
    {
        if (positions.Count > 0)
        {
            // Movement to the positions in the list.
            moveLoc = positions[0];

            //transform.LookAt(moveLoc * GetRotate() * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, moveLoc, GetMoveSpeed() * Time.deltaTime);

            if (transform.position == moveLoc)
            {
                positions.Remove(moveLoc);
            }
        }

        else if (positions.Count == 0 && directEnemy)
        {
            transform.position = Vector3.MoveTowards(transform.position, directEnemy.transform.position, GetMoveSpeed() * Time.deltaTime);
        }
    }

    // Getting the target.
    public void combat()
    {
        if (canDo)
        {
            StartCoroutine(AttackCycle());
            canDo = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag != "Ground")
        {
            foreach (ContactPoint con in col)
            {
                if (transform.tag == "Player Unit")
                {
                    angNormal = Vector3.Angle(con.point, Vector3.forward);
                    Debug.Log(angNormal);
                    Destroy(transform.GetComponent<Rigidbody>());
                    transform.GetComponent<Collider>().isTrigger = true;
                }

                else if (transform.tag == "Enemy Unit")
                {
                    transform.gameObject.GetComponent<Units>().SetFirstContact(col.gameObject.GetComponent<Units>());
                    angNormal = Vector3.Angle(con.point, Vector3.forward);
                } 
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (transform.tag == "Player Unit")
        {
            if (GetEnemy() != null)
            {
                if (col.tag != "Player Unit")
                {
                    combat();
                    Debug.Log(GetEnemy().GetHealth());
                    Debug.Log(GetHealth());
                    if (GetHealth() <= 0)
                    {
                        // Die here.
                    }
                }
            }
        }

        else if (transform.tag == "Enemy Unit")
        {
            //if (GetEnemy() != null)
            {
                if (col.tag != "Enemy Unit")
                {
                    combat();
                    Debug.Log(GetEnemy().GetHealth());
                    Debug.Log(GetHealth());
                    if (GetHealth() <= 0)
                    {
                        // Die here.
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (transform.tag == "Player Unit")
        {
            Debug.Log("Hi.");
            transform.GetComponent<Collider>().isTrigger = false;
            transform.gameObject.AddComponent<Rigidbody>();
        }
    }

    IEnumerator AttackCycle()
    {
        heightDiff = transform.position.y - GetEnemy().transform.position.y;
        GetEnemy().SetHealth(GetEnemy().GetHealth() - (GetAttDmg() + heightDiff) * GetEnemy().GetDef());
        yield return new WaitForSeconds(GetAttSpeed());
        canDo = true;
    }
}