const express = require("express");
const router = express.Router();
const avatarController = require("../controllers/avatarCtl");
const verifyToken = require("../middleware/auth");

// @router POST /api/cards/create
// @Create card
// @access private
router.post("/create", verifyToken, avatarController.create);


// @router GET /api/cards/allcards
// @Update card
// @access private
router.get("/all-avatars", verifyToken, avatarController.getall);

module.exports = router;
