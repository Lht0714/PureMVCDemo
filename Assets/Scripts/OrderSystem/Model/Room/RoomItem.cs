using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Checkin = 1,
    Leisure,
    Null=99,
} 
/// <summary>
/// �������� 
/// </summary> 
public class RoomItem 
{
    //����id
    public int id { get; set; }
    //Name
    public string name { get; set; }
    //�˿�����
    public int population { get; set; }
    //��ǰ����״̬
    public State state;
    public int price { get; set; }
    public RoomItem(int id,string name, int population, State state)
    {
        this.id = id;
        this.name = name;
        this.population = population;
        this.state = state;
    }
    public override string ToString()
    {
        return name + "\n" + returnState(state);
    }
    private string returnState(State state)
    {
        if (state.Equals(State.Checkin))
            return "����������ţ�����";
        if (state.Equals(State.Leisure))
            return "û�˿�����ס������";
        return "�Ѿ�����";
    }
}
