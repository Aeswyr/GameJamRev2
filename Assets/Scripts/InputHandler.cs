using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Button m_primary;
    public Button primary
    {
        get { return m_primary; }
    }
    private Button m_jump;
    public Button jump
    {
        get { return m_jump; }
    }

    public Button m_dashmod;
    public Button dashmod
    {
        get { return m_dashmod; }
    }

    public Button m_airdashmod;

    public Button airdashmod
    {
        get { return m_airdashmod; }
    }

    private Vector2 m_dir;
    public Vector2 dir
    {
        get { return m_dir; }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_jump.Reset();
        m_primary.Reset();
        m_dashmod.Reset();
        m_airdashmod.Reset();
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        m_jump.Set(ctx);
    }

    public void Primary(InputAction.CallbackContext ctx)
    {
        m_primary.Set(ctx);
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        m_dir = ctx.ReadValue<Vector2>();
    }

    public void DashMod(InputAction.CallbackContext ctx)
    {
        m_dashmod.Set(ctx);
        m_airdashmod.Set(ctx);
    }
}

public struct Button {
    public bool down, released, pressed;

    public void Reset() {
        released = false;
        pressed = false;
    }

    public void Set(InputAction.CallbackContext ctx) {
        down = ctx.canceled == false;

        released = !down;
        pressed = down;
    }
}
