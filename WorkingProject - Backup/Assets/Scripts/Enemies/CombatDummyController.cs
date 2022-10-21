using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
    [SerializeField]
    private float maxHealth, knockbackSpeedX, knockbackSpeedY, knockbackDuration, knockbackDeathX, knockbackDeathY, knockbackTorque;
    [SerializeField]
    private bool applyKnockback;
    [SerializeField]
    private GameObject hitParticle;

    private float currentHealth, knockbackStart;

    private int playerFacingDirection;

    private bool playerOnLeft, knockback;

    private PlayerController pc;

    private GameObject aliveGo, brokenTopGO, brokenBotGO;
    private Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBot;
    private Animator aliveAnim;

    private void Start()
    {
        currentHealth = maxHealth;

        pc = GameObject.Find("Player").GetComponent<PlayerController>();

        aliveGo = transform.Find("Alive").gameObject;
        brokenTopGO = transform.Find("Broken Top").gameObject;
        brokenBotGO = transform.Find("Broken Bottom").gameObject;

        rbAlive = aliveGo.GetComponent<Rigidbody2D>();
        rbBrokenTop = brokenTopGO.GetComponent<Rigidbody2D>();
        rbBrokenBot = brokenBotGO.GetComponent<Rigidbody2D>();

        aliveAnim = aliveGo.GetComponent<Animator>();

        aliveGo.SetActive(true);
        brokenTopGO.SetActive(false);
        brokenBotGO.SetActive(false);
    }

    private void Update()
    {
        CheckKnockback();
    }

    // After Core Combat implementation does not work. Old combat dummy. Was rewritten in CombatTestDummy.
    //private void Damage (AttackDetails attackDetails)
    //{
    //    currentHealth -= attackDetails.damageAmount;

    //    if (attackDetails.position.x < aliveGo.transform.position.x)
    //    {
    //        playerFacingDirection = 1;
    //    }
    //    else
    //    {
    //        playerFacingDirection = -1;
    //    }

    //    Instantiate(hitParticle, aliveGo.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f,360.0f)));

    //    if(playerFacingDirection == 1)
    //    {
    //        playerOnLeft = true;
    //    }
    //    else
    //    {
    //        playerOnLeft = false;
    //    }

    //    aliveAnim.SetBool("playerOnLeft", playerOnLeft);
    //    aliveAnim.SetTrigger("damage");

    //    if (applyKnockback && currentHealth > 0.0f)
    //    {
    //        Knockback();
    //    }

    //    if (currentHealth <= 0.0f)
    //    {
    //        Die();
    //    }
    //}

    private void Knockback()
    {
        knockback = true;
        knockbackStart = Time.time;
        rbAlive.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
    }

    private void CheckKnockback()
    {
        if ((Time.time >= knockbackStart + knockbackDuration) && knockback)
        {
            knockback = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }

    private void Die()
    {
        aliveGo.SetActive(false);
        brokenTopGO.SetActive(true);
        brokenBotGO.SetActive(true);

        brokenTopGO.transform.position = aliveGo.transform.position;
        brokenBotGO.transform.position = aliveGo.transform.position;

        rbBrokenBot.velocity = new Vector2(knockbackDeathX * playerFacingDirection, knockbackSpeedY);
        rbBrokenTop.velocity = new Vector2(knockbackDeathX * playerFacingDirection, knockbackDeathY);
        rbBrokenTop.AddTorque(knockbackTorque, ForceMode2D.Impulse);
    }
}
