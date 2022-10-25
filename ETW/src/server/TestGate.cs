using LogicAPI.Server.Components;
using LogicAPI.Server;
using LogicLog;
using LogicWorld.Server.Circuitry;

namespace GG
{
    public class TestGate : LogicComponent<TestGate.IData>
    {
        public interface IData
        {
            byte[] mem { get; set; }
        }
   
        
        protected override void DoLogicUpdate()
        {
            int address = 0;
            for (int i = 1; i < 6; i++)
            {
                address += Inputs[i].On ? 1 << i : 0;
            }

            byte tdata = Data.mem[address];
            
            if (Inputs[0].On)
            {
                tdata = 0;
                for (int i = 0; i < 5; i++)
                {
                    tdata += Inputs[6 + i].On ? (byte)(1 << i) : (byte)0;
                }
                Data.mem[address] = tdata;
            }


            int address2 = 0;
            for (int i = 1; i < 6; i++)
            {
                address2 += Inputs[10 + i].On ? 1 << i : 0;
            }
            byte tdata2 = Data.mem[address2];


            for (int i = 0; i < 5; i++)
            {
                Outputs[i].On = (tdata2 & (1 << i)) > 0;
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