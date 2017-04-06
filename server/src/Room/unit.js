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

//<editor-fold> State
/*
  0 = move/normal
  1 = attacking
  2 = running
*/
//</editor-fold>

  constructor(type,owner,board){
    this.type = type
    this.direction = 0
    this.owner = owner
    this.state = 0
    this.board = board
    this.assignPower()
    this.attackLoop = setInterval(this.checkAttackRange(),this.speed*750)
  }

  assignPower(){
    if (this.type==0){
      this.attack = 1
      this.speed = 3
      this.range = 1
      this.hp = 6
      this.atkSpd = 6
    }else if (this.type == 1){
      this.attack = 2
      this.speed = 2
      this.range = 2
      this.hp = 7
      this.atkSpd = 1
    }else if (this.type == 2){
      this.attack = 3
      this.speed = 1
      this.range = 1
      this.hp = 10
      this.atkSpd = 2
    }else if (this.type == 3){
      this.attack = 5
      this.speed = 3
      this.range = 1
      this.hp = 15
      this.atkSpd = 3
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

  capture(unit){
    this.target = unit
    this.state = 1
    this.attackTask = setInterval(attack(this.target),this.atkSpd*500)
  }

  attack(unit){
    unit.defense(this.attack)
  }

  defense(attack){
    let damage = attack-defense
    this.hp -= damage
    if (this.isDead()){
      this.board.units[this.x][this.y] = null
    }
    this.board.getUnit(this.x,this.y,this.x,this.y)
  }

  isDead(){
    return this.hp<=0
  }

  setPosition(x,y){
    this.x = x
    this.y = y
  }

  checkAttackRange(){
    console.log("I'm working :D");
    if (this.state == 0){
      for (var x = this.x-this.range;x<=this.x+this.range&&!this.target;x++){
        for (var y = this.y-this.range;y<=this.y+this.range&&!this.target;y++){
          if (this.board.units[x][y]!=this){
            if (this.board.units[x][y].state!=2&&this.board.units[x][y].owner != this.owner){
              this.target = this.board.units[x][y]
              this.direction = 0
              this.state = 1
            }
          }
        }
      }
    }
    if (this.state == 1){
      if (this.target.x<=this.x+this.range&&this.target.x>=this.x-this.range&&this.target.y<=this.y+this.range&&this.target.y>=this.y-this.range){
        if (this.target){
          this.attack(this.target)
        }else {
          this.state = 0
        }
      }else {
        this.target = null
        this.state = 0
      }
      this.board.getUnit(this.x,this.y,this.x,this.y)
    }
  }
}

module.exports = Unit
