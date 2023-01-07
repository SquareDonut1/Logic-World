using LogicAPI.Server.Components;
using LogicAPI.Server;
using LogicLog;
using LogicWorld.Server.Circuitry;

namespace ETW
{
    public class BitMem : LogicComponent<BitMem.IData>
    {
        public interface IData
        {
            byte[] mem { get; set; }
        }


        protected override void DoLogicUpdate()
        {      
            int address = 0;   
            for (int i = 0; i < 8; i++)
            {
                address += Inputs[i].On ? 1 << i : 0;
            }
             byte tdata = Data.mem[address];
         
                  
            if (Inputs[16].On)
            {    
                
                tdata = 0;
                for (int i = 0; i < 8; i++)
                {
                    tdata += Inputs[8 + i].On ? (byte)(1 << i) : (byte)0;
                }
                Data.mem[address] = tdata;     
            }
                             


            for (int i = 0; i < 8; i++)
            {
                Outputs[i].On = (tdata & (1 << i)) > 0;
            }
        }

        private bool _HasPersistentValues = true;

        public override bool HasPersistentValues => _HasPersistentValues;



        protected override void SetDataDefaultValues()
        {
            Data.mem = new byte[65536];
        }
    }
}