using Amazon.S3;
using Amazon.Runtime;
using Amazon;
using UnityEngine;

public class OracleStorageManager : MonoBehaviour
{
    private string accessKey = "f70559cf36d7b37e411d58a5ecb1edb2cb975698";
    private string secretKey = "YOUR_SECRET_KEY";
    
    // Format: https://{namespace}.compat.objectstorage.{region}.oraclecloud.com
    private string serviceUrl = "https://your-namespace.compat.objectstorage.us-ashburn-1.oraclecloud.com";

    private IAmazonS3 _s3Client;

    void Start()
    {
        var credentials = new BasicAWSCredentials(accessKey, secretKey);
        var config = new AmazonS3Config
        {
            ServiceURL = serviceUrl,
            ForcePathStyle = true // Required for OCI compatibility
        };

        _s3Client = new AmazonS3Client(credentials, config);
    }
}