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

  constructor(type,owner){
    this.type = type
    this.direction = 3
    this.owner = owner
  }

  setDirection(direction) {
    this.direction = direction
  }
}
