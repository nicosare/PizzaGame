using System;
using UnityEngine;

public class HideObject : ActionObject
{
    [SerializeField] private Sprite hideIcon;
    private Transform player;
    private Security security;
    private Guid guid;
    private bool isHide;
    public override Type typeOfNeededItem => throw new NotImplementedException();

    private void Awake()
    {
        security = FindObjectOfType<Security>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        OpenButton(spawnPosition, hideIcon);
    }

    private void Update()
    {
        if (isHide && !TaskManager.Instance.CheckActualTask(guid))
        {
            isHide = false;
            security.CanFind = true;
            player.gameObject.layer = 3;
            OpenButton(spawnPosition, hideIcon);
        }
    }

    public override void CancelAction()
    {
        OpenButton(spawnPosition, hideIcon);
    }

    public override void Interact()
    {
        guid = TaskManager.Instance.CreateTask(TaskAction, this, null);
    }

    public override void ItemDownCast()
    {
        throw new NotImplementedException();
    }

    public override void StartAction()
    {
        security.CanFind = false;
        isHide = true;
        player.position = transform.position;
        player.gameObject.SetActive(false);
        player.gameObject.layer = 0;
    }
}
