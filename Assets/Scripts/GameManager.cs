using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Sets the singleton.
    private GameManager instance = null;

    // List of all selected units.
    private List<Units> selectedUnit;
    private List<Units> allUnits;

    // Sets the positions.
    private Vector3 targetPos;

    // The box selection rect.
    private Rect select;

    // Raycast variables.
    private Vector3 mousePos;
    private Ray ray;
    private RaycastHit hit;
    private RaycastHit boxHit;
    private Vector3 startMouse;
    private Vector3 endMouse;
    private Vector3 centrePoint;
    public Texture box;

    // Bool list.
    private bool castRes; // Raycast results.
    private bool boxRes; // BoxCast results.
    private bool canRun; // Can run the pos capture.
    private bool cleared; // Is the list cleared.
    private bool canDo; // Can capture.
    private bool canSelect; // Is selectable.

    void Start()
    {
        // Singleton setting.
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(this.gameObject);
        }

        // Instalising the selected unit list.
        selectedUnit = new List<Units>();
        allUnits = new List<Units>();
        allUnits.AddRange(FindObjectsOfType<Units>());

        // Setting the bools.
        canSelect = true;
        canRun = false;
        cleared = false;
        canDo = true;
    }

    void Update()
    {
        // Calling functions.
        upVars();
        unitSelect();
        movePos();
        release();
    }

    public void upVars()
    {
        // Constantly updated variables.
        mousePos = Input.mousePosition;
        ray = Camera.main.ScreenPointToRay(mousePos);
        castRes = Physics.Raycast(ray, out hit);
    }

    public void unitSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMouse = mousePos;
            if (castRes)
            {
                if (hit.transform.tag == "Player Unit")
                {
                    if (canSelect)
                    {
                        // Adding the targeted unit to the select units list.
                        selectedUnit.Add(hit.transform.gameObject.GetComponent<Units>());
                        hit.transform.gameObject.GetComponentInChildren<Units>().SetIsSelect(true);
                        canRun = true;
                        canSelect = false;
                    }
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            select = new Rect(startMouse.x, startMouse.y, (mousePos.x - startMouse.x), (mousePos.y - startMouse.y));
            foreach (Units unit in allUnits)
            {
                if (unit.tag == "Player Unit")
                {
                    if (select.Contains(Camera.main.WorldToScreenPoint(unit.transform.position), true))
                    {
                        Debug.Log(unit);
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            endMouse = mousePos;
            select = new Rect(startMouse.x, startMouse.y, (endMouse.x - startMouse.x), (endMouse.y - startMouse.y));
            foreach (Units unit in allUnits)
            {
                if (unit.tag == "Player Unit")
                {
                    if (select.Contains(Camera.main.WorldToScreenPoint(unit.transform.position), true))
                    {
                        selectedUnit.Add(unit);
                        unit.GetComponentInChildren<Units>().SetIsSelect(true);
                        canRun = true;
                        canSelect = false;
                    }
                }
            }
        }

    }


    // Sets the position for the transform to move to.
    public void movePos()
    {
        if (canRun)
        {
            if (Input.GetMouseButton(1))
            {
                if (!cleared)
                {
                    // Clearing the list for new orders.
                    // Clear the positions for a selected unit.
                    foreach (Units units in selectedUnit)
                    {
                        units.GetPosList().Clear();
                        units.SetDirectEnemy(null);
                    }
                    cleared = true;
                }

                if (hit.transform.tag == "Ground" || hit.transform.tag == "Player Unit")
                {
                    if (canDo && !canSelect)
                    {
                        // Captures the target location.
                        canDo = false;
                        targetPos = new Vector3(hit.point.x, 0.5f, hit.point.z);

                        foreach (Units unit in selectedUnit)
                        {
                            unit.GetComponent<Units>().AddPos(targetPos);
                        }

                        StartCoroutine(getPos());
                    }
                }

                else if (hit.transform.tag == "Enemy Unit")
                {
                    if (canDo && !canSelect)
                    {
                        // Sets the target of an enemy and doesn't allow further positions until the a new action.
                        targetPos = new Vector3(hit.transform.position.x, 0.5f, hit.transform.position.z);
                        canDo = false;
                        foreach (Units unit in selectedUnit)
                        {
                            unit.SetDirectEnemy(hit.transform.gameObject);
                            unit.SetEnemy(hit.transform.gameObject.GetComponent<Units>());
                            Debug.Log(unit.GetDirectEnemy());
                        }

                        foreach (Units unit in allUnits)
                        {
                            if (unit.tag == "Enemy Units")
                            {
                                unit.SetEnemy(unit.GetFirstContact());
                            }
                        }
                    }
                }
            }

            else if (Input.GetMouseButtonDown(0))
            {
                if (castRes)
                {
                    if (hit.transform.tag != "Player Unit")
                    {
                        // Clears the selected units.
                        foreach (Units unit in selectedUnit)
                        {
                            unit.gameObject.GetComponentInChildren<Units>().SetIsSelect(false);
                        }
                        selectedUnit.Clear();
                        canSelect = true;
                        canRun = false;
                    }

                    else
                    {
                        selectedUnit.Clear();
                        selectedUnit.Add(hit.transform.gameObject.GetComponent<Units>());
                        hit.transform.gameObject.GetComponentInChildren<Units>().SetIsSelect(true);
                        canRun = true;
                        canSelect = false;
                    }
                }
            }
        }
    }

    // Key release bool sets.
    public void release()
    {
        if (Input.GetMouseButtonUp(1))
        {
            cleared = false;
            canDo = true;
        }
    }

    void OnGUI()
    {
        if (Input.GetMouseButton(0))
        {
            GUI.Box(new Rect(startMouse.x, startMouse.y, (mousePos.x - startMouse.x), (mousePos.y - startMouse.y)), box);
        }
    }

    // The list input frequency.
    IEnumerator getPos()
    {
        yield return new WaitForSeconds(0.01f);
        canDo = true;
    }
}