const express = require("express");
const { model, Types } = require("mongoose");
const router = express.Router();
const argon2 = require("argon2");
const jwt = require("jsonwebtoken");

const User = require("../models/User");
const Card = require("../models/Card");

const cardController = {
  getall: async (req, res) => {
    try {
      const cards = await Card.find({
        user: new Types.ObjectId(req.userId),
      }).populate("user", ["username"]);
      res.status(200).json({
        success: true,
        cards,
      });
    } catch (error) {
      res.status(500).json({
        success: false,
        message: "Internal server error",
      });
    }
  },

  createCard: async (req, res) => {
    const { name, description, level, maxlevel } = req.body;
    //validate
    if (!name) {
      return res
        .status(400)
        .json({ success: false, message: "name is missing" });
    }
    if (!description) {
      return res
        .status(400)
        .json({ success: false, message: "description is missing" });
    }
    if (!level) {
      return res
        .status(400)
        .json({ success: false, message: "level is missing" });
    }
    if (!maxlevel) {
      return res
        .status(400)
        .json({ success: false, message: "maxlevel is missing" });
    }
    try {
      const newCard = new Card({
        name,
        description,
        level,
        maxlevel,
        user: req.userId,
      });
      await newCard.save();

      res.json({
        success: true,
        message: "Add card successful!",
        post: newCard,
      });
    } catch (error) {
      res.status(500).json({
        success: false,
        message: "Internal server error",
      });
    }
  },

  gachaCard: async (req, res) => {
    function getRandomNumber(min, max) {
      const randomDecimal = Math.random();
      const randomNumber = Math.floor(randomDecimal * (max - min + 1)) + min;
      return randomNumber;
    }
    const cost = 1000;
    const user = await User.findOne({ _id: new Types.ObjectId(req.userId) });
    if (!user?.gold || user?.gold < cost) {
      return res.status(400).json({
        success: false,
        message: "Not enough gold",
      });
    }
    try {
      const randomCard = await Card.aggregate([
        {
          $match: {
            user: new Types.ObjectId(req.userId),
            $expr: { $lt: ["$level", "$maxlevel"] },
          },
        },
        {
          $sample: { size: 1 },
        },
      ]);
      if (!randomCard?.[0]) {
        return res.status(400).json({
          success: false,
          message: "All cards have reached their level limit",
        });
      }
      const newCard = {
        ...randomCard[0],
        level: getRandomNumber(randomCard[0].level + 1, randomCard[0].maxlevel),
      };
      await Card.findOneAndUpdate(
        { _id: randomCard[0] },
        { level: newCard.level }
      );
      await User.findOneAndUpdate(
        { _id: user._id },
        { gold: user.gold - cost }
      );
      res.json({
        success: true,
        message: "Successed!",
        oldCard: randomCard[0],
        newCard: newCard,
      });
    } catch (error) {
      console.log({ error });
      res.status(500).json({
        success: false,
        message: "Internal server error",
      });
    }
  },

  levelUpCard: async (req, res) => {
    const { id, cost } = req.body;
    try {
      const card = await Card.findOne({ _id: new Types.ObjectId(id) });
      const user = await User.findOne({ _id: new Types.ObjectId(req.userId) });

      if (!card) {
        return res.status(500).json({
          success: false,
          message: "Card not found",
        });
      }

      if (!user) {
        return res.status(500).json({
          success: false,
          message: "User not found",
        });
      }

      if (card?.level == card?.maxlevel) {
        return res.status(400).json({
          success: false,
          message: "Already max level",
        });
      }
      if (cost > user?.gold) {
        return res.status(400).json({
          success: false,
          message: "Not enough gold",
        });
      }

      let updatedUser = {
        gold: user.gold - cost,
      };
      let updatedCard = {
        level: card.level + 1,
      };
      await Card.findOneAndUpdate({ _id: card?._id }, updatedCard);

      const userUpdateCondition = { _id: user?._id };
      updatedUser = await User.findOneAndUpdate(
        userUpdateCondition,
        updatedUser,
        {
          new: true,
        }
      );

      return res.status(200).json({
        success: true,
        message: "Levelup success",
      });
    } catch (error) {
      res.status(500).json({
        success: false,
        message: "Internal server error",
      });
    }
  },
};

module.exports = cardController;
