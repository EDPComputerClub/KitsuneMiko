using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {
    public float attackPoint;
    public string[] initAttributes;

    SortedDictionary<int, DamageAttribute> attributes
        = new SortedDictionary<int, DamageAttribute>();

    void Awake () {
        int len = initAttributes.Length;
        DamageAttribute[] attrs = new DamageAttribute[len];
        for (int i = 0; i < len; i++) {
            System.Type attrType = System.Type.GetType(initAttributes[i]);
            attrs[i] = System.Activator.CreateInstance(attrType) as DamageAttribute;
        }
        AddAttributes(attrs);
    }

    public void AddAttributes (DamageAttribute[] attrs) {
        foreach (DamageAttribute attr in attrs) {
            attributes.Add(attr.order, attr);
        }
    }

    public void RemoveAttributes (DamageAttribute[] attrs) {
        foreach (DamageAttribute attr in attrs) {
            attributes.Remove(attr.order);
        }
    }

    public virtual void Apply (Breakable breakable) {
        float initHP = breakable.hitPoint;
        breakable.hitPoint -= breakable.defencePoint * attackPoint;
        foreach (DamageAttribute attr in attributes.Values) {
            attr.Apply(this, breakable, initHP);
        }
    }
}

public abstract class DamageAttribute {
    public int order;

    public abstract void Apply (Damage damage, Breakable breakable, float initHP);
}
