using Pokemon.Constants;

namespace Pokemon.Packets.Outgoing
{
    public class MovePacket : OutgoingPacket
    {
        public Constants.Direction Direction { get; set; }

        public MovePacket(Objects.Client c, Constants.Direction direction)
            : base(c)
        {
            Direction = direction;

            switch (direction)
            {
                case Pokemon.Constants.Direction.Down:
                    Type = OutgoingPacketType.MoveDown;
                    break;
                case Pokemon.Constants.Direction.Up:
                    Type = OutgoingPacketType.MoveUp;
                    break;
                case Pokemon.Constants.Direction.Right:
                    Type = OutgoingPacketType.MoveRight;
                    break;
                case Pokemon.Constants.Direction.Left:
                    Type = OutgoingPacketType.MoveLeft;
                    break;
                case Pokemon.Constants.Direction.DownLeft:
                    Type = OutgoingPacketType.MoveDownLeft;
                    break;
                case Pokemon.Constants.Direction.DownRight:
                    Type = OutgoingPacketType.MoveDownRight;
                    break;
                case Pokemon.Constants.Direction.UpLeft:
                    Type = OutgoingPacketType.MoveUpLeft;
                    break;
                case Pokemon.Constants.Direction.UpRight:
                    Type = OutgoingPacketType.MoveUpRight;
                    break;
            }

            Destination = PacketDestination.Server;
        }

        public override bool ParseMessage(NetworkMessage msg, PacketDestination destination)
        {
            return true;
        }

        public override void ToNetworkMessage(NetworkMessage msg)
        {
            msg.AddByte((byte)Type);
        }

        public static bool Send(Objects.Client client, Constants.Direction direction)
        {
            MovePacket p = new MovePacket(client, direction);
            return p.Send();
        }
    }
}