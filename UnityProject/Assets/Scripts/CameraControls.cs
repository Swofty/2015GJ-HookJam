﻿using UnityEngine;using System.Collections;using System;public class CameraControls : MonoBehaviour {
    /* A bunch of stuff that relates to how the camera shakes*/
    /**     * Camera "shake" effect preset: shake camera on both the X and Y axes.     */
    public const uint SHAKE_BOTH_AXES = 0;
    /**     * Camera "shake" effect preset: shake camera on the X axis only.     */
    public const uint SHAKE_HORIZONTAL_ONLY = 1;
    /**     * Camera "shake" effect preset: shake camera on the Y axis only.     */
    public const uint SHAKE_VERTICAL_ONLY = 2;	float _fxShakeIntensity = 0.0f;	float _fxShakeDuration = 0.0f;	uint _fxShakeDirection = 0;    Action _fxShakeComplete = null;	Vector2 _fxShakeOffset = new Vector2();    //Vector3 _fxInitialPosition = new Vector3();

    float Z_OFFSET = -10;

    public GameObject player;	// Use this for initialization	void Start () {
        transform.position = player.transform.position + new Vector3(0, 0, Z_OFFSET);	}		// Update is called once per frame	void Update () {        if (Input.GetKeyDown(KeyCode.Space))        {            shake();        }



        if (transform.position != player.transform.position + new Vector3(0, 0, Z_OFFSET))
        {
            float x = ((player.transform.position + new Vector3(0, 0, Z_OFFSET)) - transform.position).x;
            float y = ((player.transform.position + new Vector3(0, 0, Z_OFFSET)) - transform.position).y;
            rigidbody2D.velocity = new Vector2(x * 5.0f, y * 5.0f);
        }
        else
        {
            rigidbody2D.velocity.Set(0.0f, 0.0f);
        }        //Update the "shake" special effect        if (_fxShakeDuration > 0)        {            _fxShakeDuration -= Time.deltaTime;            if (_fxShakeDuration <= 0)            {                _fxShakeOffset.Set(0, 0);                if (_fxShakeComplete != null)                    _fxShakeComplete();            }            else            {                if ((_fxShakeDirection == SHAKE_BOTH_AXES) || (_fxShakeDirection == SHAKE_HORIZONTAL_ONLY))                    _fxShakeOffset.x = (UnityEngine.Random.Range(-1.0F, 1.0F) * _fxShakeIntensity); //gotta be able to shift the games screen by some percent?;                if ((_fxShakeDirection == SHAKE_BOTH_AXES) || (_fxShakeDirection == SHAKE_VERTICAL_ONLY))                    _fxShakeOffset.y = (UnityEngine.Random.Range(-1.0F, 1.0F) * _fxShakeIntensity); //gotta be able to shift the games screen by some percent?;;            }            print("we visted this many times" + _fxShakeDuration);        }        if ((_fxShakeOffset.x != 0) || (_fxShakeOffset.y != 0))        {            float x = transform.position.x;            float y = transform.position.y;            float z = transform.position.z;            print(_fxShakeOffset.x);            print(_fxShakeOffset.y);            transform.position = new Vector3(x + _fxShakeOffset.x, y + _fxShakeOffset.y, z);            print("hello");        }        //May not need to fix the camera again once we get the camera to follow a player        /*        else        {            float x = _fxInitialPosition.x;            float y = _fxInitialPosition.y;            float z = _fxInitialPosition.z;            this.transform.position.Set(x, y, z);        }        */	}    void shake(float Intensity = 0.05f, float Duration = 0.5f, Action OnComplete = null, bool Force = true, uint Direction = 0)    {        if(!Force && ((_fxShakeOffset.x != 0) || (_fxShakeOffset.y != 0)))			return;		_fxShakeIntensity = Intensity;		_fxShakeDuration = Duration;        _fxShakeComplete = OnComplete;		_fxShakeDirection = Direction;        _fxShakeOffset.Set(0, 0);    }}