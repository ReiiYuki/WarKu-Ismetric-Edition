class Board {
//<editor-fold> Constructor
  constructor(){
    this.SIZE = 16
    this.createFloor()
  }
//</editor-fold>

//<editor-fold> Unit

//<editor-fold> SpawnUnit
  spawnUnit(remote,x,y,type){
    if (this.isSpawnZone(remote,x,y)){
      this.units[x][y] = type+","+3
    }
    getUnit(x,y)
  }

  isSpawnZone(remote,x,y){
    return y==this.SIZE-1 && [0,1,10,12,13,14,16].indexOf(this.floors[x][y])>=0 && !this.units[x][y]
  }
//</editor-fold>

//<editor-fold> MoveUnit
  updateUnit(x,y){
    if (direction==1) this.units[x-1][y] = this.units[x][y].split(",")[0]+","+0
    else if (direction==2) this.units[x+1][y] = this.units[x][y].split(",")[0]+","+0
    else if (direction==3) this.units[x][y-1] = this.units[x][y].split(",")[0]+","+0
    else if (direction==4) this.units[x][y+1] = this.units[x][y].split(",")[0]+","+0
    if (direction!=0) delete this.units[x][y]
  }

  moveUnit(x,y,direction){
      this.units[x][y] = this.units[x][y].split(",")[0]+","+direction
  }

  getUnit(x,y){
    //TODO Send unit to Client
  }
//</editor-fold>

//</editor-fold>

//<editor-fold> Formatting

  formatFloors(){
    let str = ""
    for (let i = 0;i<this.SIZE;i++){
      for (let j  =0;j<this.SIZE;j++){
        str+= this.floors[i][j]+" "
      }
    }
    return str
  }

  formatUnits(){
    let str = ""
    for (let i = 0;i<this.SIZE;i++){
      for (let j  =0;j<this.SIZE;j++){
        str+= this.units[i][j]+" "
      }
    }
    return str
  }

//</editor-fold>

// <editor-fold> Place Tile

  createFloor(){
    this.floors = []
    this.units = []
    for (let i = 0;i<this.SIZE;i++){
      this.floors.push([])
      this.units.push([])
      for (let j = 0;j<this.SIZE;j++){
        this.floors[i].push(0)
        this.units[i].push(null)
      }
    }
    this.placeRiver()
    this.placeMountain()
    this.placeForest()
    this.placeStone()
  }

  placeRiver(){
    if (Math.round(Math.random())==1){
      let y = Math.floor((Math.random()*this.SIZE))
      let x = 0
      let state = [4,6,8][Math.floor(Math.random()*3)]
      this.floors[x][y] = state
      if (state==4) y++
      else if (state==6) y--
      else if (state==8) x++
      while (x<this.SIZE&&y<this.SIZE&&x>=0&&y>=0){
        if (state==5||state==8) state = [4,6,8][Math.floor(Math.random()*3)]
        else if (state==3) state = [4,8][Math.floor(Math.random()*2)]
        else if (state==4||state==7) state = [7,5][Math.floor(Math.random()*2)]
        else if (state==6) state = 3
        this.floors[x][y] = state
        if (state==3||state==5||state==8) x++
        else if (state==4||state==7) y++
        else if (state==6) y--
      }
    }
  }

  placeMountain(){
    let numMount = Math.floor(Math.random()*11)
    for (let i =0;i<numMount;i++ ){
      var x = Math.floor(Math.random()*this.SIZE)
      var y = Math.floor(Math.random()*this.SIZE)
      if (this.floors[x][y]==0||[9,10,11,12,14,15,16,17].indexOf(this.floors[x][y])>0){
        this.floors[x][y] = 13
        if (x-1>=0&&y-1>=0&&this.floors[x-1][y-1]==0) this.floors[x-1][y-1] = 9
        if (x-1>=0&&this.floors[x-1][y]==0) this.floors[x-1][y] = 10
        if (x-1>=0&&y+1<this.SIZE&&this.floors[x-1][y+1]==0) this.floors[x-1][y+1] = 11
        if (y-1>=0&&this.floors[x][y-1]==0) this.floors[x][y-1] = 12
        if (y+1<this.SIZE&&this.floors[x][y+1]==0) this.floors[x][y+1] = 14
        if (x+1<this.SIZE&&y-1>=0&&this.floors[x+1][y-1]==0) this.floors[x+1][y-1] = 15
        if (x+1<this.SIZE&&this.floors[x+1][y]==0) this.floors[x+1][y] = 16
        if (x+1<this.SIZE&&y+1<this.SIZE&&this.floors[x+1][y+1]==0) this.floors[x+1][y+1] = 17
      }
    }
  }

  placeForest(){
    for (let i = 0;i<Math.floor(Math.random()*20);i++){
      var x = Math.floor(Math.random()*this.SIZE)
      var y = Math.floor(Math.random()*this.SIZE)
      if (this.floors[x][y]==0) this.floors[x][y] = 1
    }
  }

  placeStone(){
    for (let i = 0;i<Math.floor(Math.random()*10);i++){
      var x = Math.floor(Math.random()*this.SIZE)
      var y = Math.floor(Math.random()*this.SIZE)
      if (this.floors[x][y]==0) this.floors[x][y] = 2
    }
  }

// </editor-fold>

}

//<editor-fold> Tile Description
/**
0 = Normal Tile *
1 = Forest Tile *
2 = Stone Tile
3 = RiverRightUp
4 = RiverLeftUp
5 = RiverRightDown
6 = RiverLeftDown
7 = RiverDown
8 = RiverLeft
9 = MountainRidgeRightUp
10 = MountainSlopeRight *
11 = MountainRidgeRightDown
12 = MountainSlopeUp *
13 = MountainPeak *
14 = MountainSlopeDown *
15 = MountainRidgeLeftUp
16 = MountainSlopeLeft *
17 = MountainRidgeLeftDown
**/
//</editor-fold>

module.exports = Board
