public class TilePosition{
    public int x;
    public int y;
    
    public TilePosition(int x, int y){
        this.x = x;
        this.y = y;
    }

    public override int GetHashCode() {
        var hashCode = -307843816;
        hashCode = hashCode * -1521134295 + x.GetHashCode();
        hashCode = hashCode * -1521134295 + y.GetHashCode();
        return hashCode;
    }
    public override bool Equals(object obj) {
        return Equals(obj as TilePosition);
    }
    public bool Equals(TilePosition obj) {
        return obj != null && obj.x == x && obj.y == y;
    }
}