using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Native C# Class
public class GlobalServices
{
    //  Service Instances
    public IEventBus EventBus { get; private set; }   
    
    public void Initialize()
    {
        EventBus = new EventBus();
    }

    public void Dispose()
    {
        
    }
}
