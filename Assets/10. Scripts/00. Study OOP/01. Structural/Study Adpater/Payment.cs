using UnityEngine;

namespace Study.OOP._01._Structural.Study_Adpater
{
    //외부 SDK나 API라고 가정을 하고 아래 내용을 참고 하세요
    // 카카오페이랑 라인페이를 연동하는 어댑터 클래스들을 만들어 보겠습니다

    #region ExternalAPI
    
    public class ExternalKakaoAPI
    {
        // 카카오의 결제 API는 단순 요청만 가지고 동작할수있게끔
        // 디자인 되어있다고 가정하겠습니다

        public bool Request(string itemCode, string kakaoID)
        {
            Debug.Log($"[KakaoPay API] : {kakaoID} 유저가 " +
                      $"{itemCode} 상품 결제 요청");
            return true;
        }
        
    }

    public class ExternalLineAPI
    {
        // 라인의 결제 수단은 인증을 한번 거치고, 그다음에 결제요청
        //을 수행할 수 있게끔 디자인 되어있다고 가정하겠습니다. 

        public string GetAuthToken(string lineID)
        {
            return "LINE_TOKEN_XYZ";
        }

        public void ExecutePayment(string token, string productId, string amount)
        {
            Debug.Log($"[LinePay API] : {productId} 상품 {amount} 결제 실행." +
                      $"{token}");
        }
    }
    #endregion
    
    
    
}
