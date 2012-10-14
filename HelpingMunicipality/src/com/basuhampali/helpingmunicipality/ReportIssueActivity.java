package com.basuhampali.helpingmunicipality;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.Camera;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.os.Environment;
import android.view.View;
import android.widget.Toast;

public class ReportIssueActivity extends Activity {

    private static final int CAMERA_REQUEST = 0;

	@Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.report_issue);
        LocationManager mlocManager =

        	(LocationManager)getSystemService(Context.LOCATION_SERVICE);

        	LocationListener mlocListener = new MyLocationListener();


        	mlocManager.requestLocationUpdates( LocationManager.GPS_PROVIDER, 0, 0, mlocListener);
    }
    
     public void takeSnapOnClick(View view){


    	 Intent cameraIntent=new Intent(android.provider.MediaStore.ACTION_IMAGE_CAPTURE);
    	 startActivityForResult(cameraIntent, CAMERA_REQUEST);
    }
     
     public class MyLocationListener implements LocationListener

     {

     @Override

     public void onLocationChanged(Location loc)

     {

     loc.getLatitude();

     loc.getLongitude();

     String Text = "My current location is: " + 

     "Latitud = " + loc.getLatitude() +

     "Longitud = " + loc.getLongitude();


     Toast.makeText( getApplicationContext(),

     Text,

     Toast.LENGTH_SHORT).show();

     }


     @Override

     public void onProviderDisabled(String provider)

     {

     Toast.makeText( getApplicationContext(),

     "Gps Disabled",

     Toast.LENGTH_SHORT ).show();

     }


     @Override

     public void onProviderEnabled(String provider)

     {

     Toast.makeText( getApplicationContext(),

     "Gps Enabled",

     Toast.LENGTH_SHORT).show();

     }


     @Override

     public void onStatusChanged(String provider, int status, Bundle extras)

     {


     }

     }/* End of Class MyLocationListener */
    
	
     
     protected void onActivityResult(int requestCode, int resultCode, Intent data) {
    	 if (requestCode == CAMERA_REQUEST  && resultCode == RESULT_OK) { 
    		
             Bitmap bitmap = (Bitmap) data.getExtras().get("data"); 
             
             ByteArrayOutputStream bytes = new ByteArrayOutputStream();
             bitmap.compress(Bitmap.CompressFormat.PNG, 40, bytes);

             //you can create a new file name "test.jpg" in sdcard folder.
             File f = new File(Environment.getExternalStorageDirectory()
                                     + File.separator + "test1.png");
             try {
				f.createNewFile();
				 //write the bytes in file
				 FileOutputStream fo = new FileOutputStream(f);
				 fo.write(bytes.toByteArray());
			} catch (FileNotFoundException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
             
    	
  }
     }
    	    	
}
