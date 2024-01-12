const express = require("express");
const { model, Types } = require("mongoose");
const router = express.Router();
const argon2 = require("argon2");
const jwt = require("jsonwebtoken");

const User = require("../models/User");

const userController = {
  updateProfile: async (req, res) => {
    const { username, fullName, email, avatar } = req.body;
    let updatedUser = {
    };
    if (username) {
      updatedUser = { ...updatedUser, username}
    }
    if (fullName) {
      updatedUser = { ...updatedUser, fullName}
    }
    if (email) {
      updatedUser = { ...updatedUser, email}
    }
    try {
      //Get user
      const user = await User.findOne({ userId: req.userId });

      /*      if (user.username != username) {
              return res
                .status(400)
                .json({ success: false, message: "Cannot change Username" });
            }*/

      //all good
      if (avatar) {
        updatedUser = { ...updatedUser, avatar: avatar }
      }

      const userUpdateCondition = { _id: req.userId };
      updatedUser = await User.findOneAndUpdate(
        userUpdateCondition,
        updatedUser,
        {
          new: true,
        }
      );

      return res
        .status(200)
        .json({ success: true, message: "Update successful!" });
    } catch (err) {
      res.status(500).json(err.msg);
    }
  },

  updatePassword: async (req, res) => {
    const { password } = req.body;
    if (!password) {
      return res
        .status(400)
        .json({ success: false, message: "Missing password" });
    }

    try {
      //all good
      const hashesPassword = await argon2.hash(password);
      let updatedUser = {
        password: hashesPassword,
      };

      const userUpdateCondition = { _id: req.userId };
      updatedUser = await User.findOneAndUpdate(
        userUpdateCondition,
        updatedUser,
        {
          new: true,
        }
      );

      return res
        .status(200)
        .json({ success: true, message: "Change password successful!" });
    } catch (err) {
      res.status(500).json(err.msg);
    }
  },

  updateGold: async (req, res) => {
    const { gold } = req.body;
    if (!gold) {
      return res.status(400).json({ success: false, message: "Missing gold" });
    }

    try {
      //all good

      let updatedUser = {
        gold,
      };

      const userUpdateCondition = { _id: req.userId };
      updatedUser = await User.findOneAndUpdate(
        userUpdateCondition,
        updatedUser,
        {
          new: true,
        }
      );

      return res
        .status(200)
        .json({ success: true, message: "Update gold successful!" });
    } catch (err) {
      res.status(500).json(err.msg);
    }
  },

  get: async (req, res) => {
    try {
      const user = await User.findOne({ _id: req.userId });
      console.log({ req });
      return res.status(200).json({ success: true, user });
    } catch (err) {
      res.status(500).json(err.msg);
    }
  },
};

module.exports = userController;
