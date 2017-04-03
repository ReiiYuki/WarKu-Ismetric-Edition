class Unit {


  //<editor-fold> Direction Description
  /*
          STOP = 0,
          RIGHT = 1,
          LEFT = 2,
          UP = 3,
          DOWN = 4
  */
  //</editor-fold>

//<editor-fold> Unit Type Description
/*
  0 = Worker
  1 = Archer
  2 = Swordman
  3 = Lancer
*/
//</editor-fold>

  constructor(type,owner){
    this.type = type
    this.direction = 0
    this.owner = owner
    this.assignPower()
  }

  assignPower(){
    if (this.type==0){
      this.attack = 1
      this.speed = 1
      this.range = 1
      this.hp = 6
      this.atkSpd = 1
    }else if (this.type == 1){
      this.attack = 2
      this.speed = 3
      this.range = 2
      this.hp = 7
      this.atkSpd = 5
    }else if (this.type == 2){
      this.attack = 3
      this.speed = 3
      this.range = 1
      this.hp = 10
      this.atkSpd = 3
    }else if (this.type == 3){
      this.attack = 5
      this.speed = 1
      this.range = 1
      this.hp = 15
      this.atkSpd = 2
    }
  }

  hide(){
    this.isHide = !this.isHide
  }

  getDirection(remote){
    if (this.owner.playerNum != remote.playerNum){
      if (this.owner.playerNum == 0) return this.inverseDirection(this.direction)
      return this.direction
    }
    if (this.owner.playerNum == 0) return this.direction
    return this.inverseDirection(this.direction)
  }

  inverseDirection(direction){
    if (direction==1) return 2
    else if (direction==2) return 1
    else if (direction==3) return 4
    else if (direction==4) return 3
  }

  isOwner(remote) {
    return this.owner == remote
  }
}

module.exports = Unit
