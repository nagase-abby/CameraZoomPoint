using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Vector2 origin = Vector2.zero;

    [SerializeField]
    private Transform point = null;

    [SerializeField]
    private Camera cam = null;

    [SerializeField]
    private Cinemachine.Examples.MouseScrollZoom2D zoomInfo = null;

    private void Start()
    {
        origin = transform.position;
    }

    private void LateUpdate()
    {
        var cameraSize = cam.orthographicSize;

        // 現在のカメラサイズが指定のサイズ範囲の何%かを求める
        var percent = GetCameraPercent(cameraSize);
        Debug.Log($"現在のカメラサイズ:{cameraSize}でカメラ範囲{zoomInfo.MinZoom} - {zoomInfo.MaxZoom}の時、全体の{percent * 100}%にあたります");

        // 求めたカメラサイズ割合から原点座標から目標座標のどのくらいの位置になるかをもとめる
        // 例(8,2) -> (100,100)
        // 座標範囲内の割合を求めて足し合わせた座標をカメラサイズ座標
        var goleX = origin.x + (point.position.x - origin.x) * percent;
        var goleY = origin.y + (point.position.y - origin.y) * percent;
        Debug.Log($"現在のカメラサイズ:{cameraSize}におけるカメラ座標は:({goleX},{goleY},{transform.position.z})");

        transform.position = new Vector3(goleX, goleY, -10);
    }

    /// <summary>
    /// 現在のカメラサイズが指定のサイズ範囲の何%かを求める
    /// </summary>
    private float GetCameraPercent(float cameraSize)
    {
        // カメラ最大値・最小値
        var maxSize = zoomInfo.MaxZoom;
        var minSize = zoomInfo.MinZoom;

        // 範囲を0から最大値に変換
        var rangeMax = maxSize - minSize;

        // 現在サイズが最小値からどのくらい離れているか / 0(最小値)から最大値丸め
        var rangeCameraSize = cameraSize - minSize;

        // 現在のカメラサイズが0から最大値の何%に当たるか
        // 0のとき100%とする
        var percent = 100 - (rangeCameraSize / rangeMax * 100);
        return percent / 100;
    }
}
