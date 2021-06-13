import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.lang.ClassNotFoundException;
import java.net.ServerSocket;
import java.net.Socket;
import java.sql.*;
import java.util.Date;


public class Server {
 
    static final String DB_URL = "jdbc:mysql://localhost/moisture";
    static final String USER = "root";
    static final String PASS = "San@12345";
    

    private static ServerSocket server; //create object from ServerSocket

    private static int port = 8000; //port

    public static void main(String[] args) throws IOException, ClassNotFoundException{
        server = new ServerSocket(port);

        while(true){ //endless loop
            System.out.println("requesting...");

            Socket socket = server.accept();
            ObjectInputStream ois = new ObjectInputStream(socket.getInputStream()); //get data from client
            String message = (String) ois.readObject(); //convert it into the string type
            System.out.println(message);
            ObjectOutputStream oos = new ObjectOutputStream(socket.getOutputStream()); //send data to the client
            oos.writeObject("Data successfully stored"+message); // send response
	
            if(message != null && !message.equals("")){
             insert_data_to_the_db(message);
	    }

            ois.close();
            oos.close();
            socket.close();
            if(message.equalsIgnoreCase("exit")) break;
        }
        System.out.println("close");

        server.close();
    }
   public static void insert_data_to_the_db (String data){

      try {
	 Connection conn = DriverManager.getConnection(DB_URL, USER, PASS);
         Statement stmt = conn.createStatement();
	 String query = "INSERT INTO sensor_Val (Time, Value) VALUES (" + new Date() + "," + Integer.parseInt(String.valueOf(data)) + ")";
         stmt.executeUpdate(query);
      } catch (SQLException e) {
         e.printStackTrace();
      } 
   }
}
