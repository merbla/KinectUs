module KinectUs.FSharp

open fszmq
open fszmq.Context
open fszmq.Socket



[<AutoOpen>]
module private Utilities =

  (* timing functions *)
  open System.Runtime.InteropServices
  open System.Configuration

  [<DllImport("libzmq",CallingConvention=CallingConvention.Cdecl)>]
  extern nativeint zmq_stopwatch_start()
  
  [<DllImport("libzmq",CallingConvention=CallingConvention.Cdecl)>]
  extern uint32 zmq_stopwatch_stop(nativeint watch)

  (* program return codes *)
  let [<Literal>] OKAY = 0
  let [<Literal>] FAIL = 3

  (* I/O helpers *)
  let scanln = System.Console.ReadLine
  let encode = string >> System.Text.Encoding.ASCII.GetBytes
  let decode = System.Text.Encoding.ASCII.GetString

  let subscriberPort = System.Configuration.ConfigurationManager.AppSettings.["ZeroMQJsonSubscribePort"]
  let syncPort = System.Configuration.ConfigurationManager.AppSettings.["ZeroMQSyncPort"]
  //let ZeroMQSyncPort 

  let prompt msg = 
    printf "%s " msg
    scanln ()

[<EntryPoint>]
let main args = 
  let result = ref OKAY
  try
    use context = new Context(1)
    
    // first, connect our subscriber socket
    use subscriber = sub context
    "tcp://localhost:5561" |> connect subscriber
    [ ""B ] |> subscribe subscriber
    
    // second, synchronize with publisher
    use syncclient = req context
    "tcp://localhost:5562" |> connect syncclient

    // - send a synchronization request
    "" |> encode |>> syncclient   

    printfn "Sent Sync" 
    // - wait for synchronization reply
    syncclient |> recv |> ignore

    // third, get our updates and report how many we got
    let rec loop count =
        let reply = subscriber|> recv |> decode
        printf "%d" count
        if  reply <> "END" 
            then loop (count + 1)
            else count
                
    let update_nbr = loop 0
    printfn "Received %d updates" update_nbr
    
     
  with
  | x ->  result := FAIL
          printfn "FAIL: %s" x.Message
            
  printf "press <return> to exit... "
  scanln () |> ignore
  exit !result
