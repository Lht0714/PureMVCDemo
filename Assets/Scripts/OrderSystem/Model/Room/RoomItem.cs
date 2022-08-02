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
/// 房间数据 
/// </summary> 
public class RoomItem 
{
    //房间id
    public int id { get; set; }
    //Name
    public string name { get; set; }
    //顾客数量
    public int population { get; set; }
    //当前房间状态
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
            return "有人请勿打扰！！！";
        if (state.Equals(State.Leisure))
            return "没人可以入住！！！";
        return "已经结账";
    }
}
