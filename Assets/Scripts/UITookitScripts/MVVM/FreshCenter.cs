using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreshCenter : MonoBehaviour
{
    public TestUIModel TestUiModel;

    public TestUIViewModel TestUiViewModel;
    // Update is called once per frame
    void Update()
    {
        TestUiModel.Fresh();
        TestUiViewModel.Fresh();
    }
}
