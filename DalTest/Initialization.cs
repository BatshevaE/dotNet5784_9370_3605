﻿namespace DalTest;
using DalApi;
using DO;

public static class Initialization
{
    private static ITask? s_dalTask; //stage 1
    private static IEngineer? s_dalEngineer; //stage 1
    private static IDependency? s_dalDependency; //stage 1
    private static readonly Random s_rand = new();//The entities will use this random in order to fill the objects values

}